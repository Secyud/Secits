import SContainer from "./SContainer";
import type {IExtendStyle, IHasContent, IHasSize, IHasTheme} from "../abstraction";
import type {MouseEventHandler} from "react";

export default function SButton(
    props:
        IHasTheme &
        IHasSize &
        IHasContent &
        IExtendStyle & {
        onClick?: MouseEventHandler<HTMLDivElement>
    }
) {
    return (
        <SContainer componentName={"s-button"} {...props}/>
    );
}