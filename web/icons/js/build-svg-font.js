import {SVGIcons2SVGFontStream} from "svgicons2svgfont";
import fs from "fs";
import {join} from "node:path";
import {writeFile} from "node:fs/promises";


function buildSvgFontFromConfig(config, inputPath, outputPath) {
    return new Promise((resolve, reject) => {
        let fontName = config["fontName"];
        let texts = config["fonts"];

        let css = ".si{";

        const fontStream = new SVGIcons2SVGFontStream({
            fontName: fontName
        });

        fontStream.pipe(fs.createWriteStream(outputPath))
            .on('finish', function () {
                console.log(`SvgFont successfully created!(${outputPath})`);
                resolve();
            })
            .on('error', function (err) {
                console.log(err);
                reject(err);
            });

        for (const name in texts) {
            let unicode = texts[name];
            let glyph = fs.createReadStream(join(inputPath, name + '.svg'));
            glyph.metadata = {
                unicode: [unicode],
                name: name,
            }

            css += `&.${name}::before {content: '${unicode}';}`;
            fontStream.write(glyph);
        }

        fontStream.end();
        css += '}';
        writeFile(join(outputPath, "../icons.css"), css).then(r => {
        });
    })
}

export function buildSvgFont(inputPath, outputPath) {
    return new Promise((resolve, reject) => {
        fs.readFile(inputPath, "utf8", (err, data) => {
            if (err) {
                console.log(err);
                return;
            }
            let str = data.toString();
            str = str.substring(str.indexOf("{"));
            buildSvgFontFromConfig(JSON.parse(str), join(inputPath, '..'), outputPath)
                .then(r => {
                    resolve();
                });
        })
    });
}