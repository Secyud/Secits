import SContainer from "./SContainer";
import type {IExtendStyle, IHasContent, IHasSize, IHasTheme} from "../abstraction";

export default function SCardHeader(
    props:
        IHasTheme &
        IHasSize &
        IHasContent &
        IExtendStyle
) {
    return (
        <SContainer componentName={"s-card-header"} {...props}/>
    );
}