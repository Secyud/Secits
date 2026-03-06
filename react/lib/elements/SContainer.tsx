import type {IHasContent, IIsComponent} from "../abstraction";
import {spreadProps} from "../abstraction"

export default function SContainer(props: IHasContent & IIsComponent) {
    return (
        <div {...spreadProps(props)}/>
    );
}