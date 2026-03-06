import type {IExtendStyle, IHasContent, IHasSize, IHasTheme} from "../abstraction";
import {spreadProps} from "../abstraction";

export default function SIcon(
    props:
        IHasTheme &
        IHasSize &
        IHasContent &
        IExtendStyle & {
        icon?: string
    }
) {
    return (
        <i  {...spreadProps(props, "s-icon", props.icon)}/>
    );
}