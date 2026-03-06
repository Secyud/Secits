import type {CSSProperties, ReactNode} from "react";

export const STheme = {
    Default: null,
    Primary: "primary",
    Secondary: "secondary",
    Naive: "naive",
    Success: "success",
    Info: "info",
    Warning: "warning",
    Danger: "danger",
} as const;

export const SSize = {
    Default: null,
    XSmall: "xs",
    Small: "sm",
    Medium: "md",
    Large: "lg",
    XLarge: "xl",
} as const;

export interface IHasTheme {
    theme?: typeof STheme[keyof typeof STheme];
}

export interface IHasSize {
    size?: typeof SSize[keyof typeof SSize];
}

export interface IHasContent {
    children?: ReactNode
}

export interface IIsComponent {
    componentName: string
}

export interface IExtendStyle {
    className?: string;
    style?: CSSProperties;
}

export function spreadProps(props: IHasSize & IHasTheme & IExtendStyle &
    {
        componentName?: string,
    }, ...classNames: (string | null | undefined)[]) {
    const {componentName, theme, size, ...res} = props;

    res.className = [...classNames, componentName, theme, size, props.className]
        .filter(u => u && u !== "")
        .join(" ");
    return res;
}