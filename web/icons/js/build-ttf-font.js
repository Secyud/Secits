import fs from "fs";
import svg2ttf from "svg2ttf";

export function buildTtfFont(svgPath, outputPath) {
    return new Promise((resolve, reject) => {
        let ttf = svg2ttf(fs.readFileSync(svgPath, 'utf8'), {});
        fs.writeFile(outputPath, Buffer.from(ttf.buffer), async (err, data) => {
            if (err) {
                console.log(err);
                return false;
            }

            console.log(`Ttf icon successfully created!(${outputPath})`);
            resolve();
        });
    });
}