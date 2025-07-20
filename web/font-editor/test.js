import {svgEditor} from "./svg-editor.js";
import fs from "fs";
import {join} from "node:path";
import {fileURLToPath} from "url";
import {dirname} from "path";

const __filename = fileURLToPath(import.meta.url);
const __dirname = dirname(__filename);

fs.readFile(join(__dirname, "test.json"), "utf8", (err, data) => {
    if (err) {
        console.log(err);
        return;
    }
    let str = data.toString();
    str = str.substring(str.indexOf("{"));

    let canvas = JSON.parse(str);

    fs.writeFile(join(__dirname, "test.svg"), svgEditor.generateSvg(canvas), "utf8", (err) => {})
})