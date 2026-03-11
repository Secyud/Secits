import {FontBuilder} from "../../../dragert/fonts/src/font-builder"
import {fs} from "../../../dragert/utils/src/file-stream";
import * as path from "path";
import {join} from "node:path";

const __dirname = path.resolve();
const folder = join(__dirname, '../icons');
console.log("Current dir: " + folder);
const jsonConfigStr = await fs.readFile(join(folder, "secits-icon.json"), {encoding: "utf-8"}) as string;
const jsonConfig = JSON.parse(jsonConfigStr.substring(jsonConfigStr.indexOf("{")));
console.log(jsonConfig);

let builder = new FontBuilder();

let fonts = jsonConfig["fonts"];
for (const key in fonts) {
    builder.addSvg({
        name: key, path: join(folder, "svg", "solid", key + ".svg"), unicode: fonts[key]
    })
}

const fileName = "si-solid";

await builder.buildSvgFontCss(join(folder, "../css/icons", fileName + ".less"), "sis");
await builder.buildSvg(join(folder, "svg", fileName + ".svg"), fileName);
await builder.buildTtfFont(join(folder, "svg", fileName + ".svg"), join(folder, "tff", fileName + ".ttf"))
await builder.buildWoff2Font(join(folder, "tff", fileName + ".ttf"), join(folder, "woff2", fileName + ".woff2"))