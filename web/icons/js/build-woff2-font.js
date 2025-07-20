import { readFile, writeFile } from 'node:fs/promises';
import ttf2woff2 from 'ttf2woff2';

export async function buildWoff2Font(ttfPath, outputPath) {
    const input = await readFile(ttfPath);
    await writeFile(outputPath, ttf2woff2(input));
    console.log(`Woff2 icon successfully created!(${outputPath})`);
}