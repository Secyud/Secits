import type {IExtendStyle, IHasContent, IHasSize, IHasTheme} from "../abstraction";
import {spreadProps} from "../abstraction";

export default function SInput(
    props:
        IHasTheme &
        IHasSize &
        IHasContent &
        IExtendStyle
) {
    return (
        <input {...spreadProps(props, "s-input")}/>
    );
}