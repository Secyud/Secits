import SContainer from "./SContainer";
import type {IExtendStyle, IHasContent, IHasSize, IHasTheme} from "../abstraction";

export default function SCard(
    props:
        IHasTheme &
        IHasSize &
        IHasContent &
        IExtendStyle
) {
    return (
        <SContainer componentName={"s-card"} {...props}/>
    );
}