import * as React from "react";

export function Card({children, className}: {
    children: React.ReactNode,
    className?: string
}) {
    if (className) {
        className = "s-card " + className;
    } else {
        className = "s-card";
    }

    return (
        <div className={className}>
            {children}
        </div>
    );
}

export function CardHeader({children, className}: {
    children: React.ReactNode,
    className?: string
}) {
    if (className) {
        className = "s-card " + className;
    } else {
        className = "s-card";
    }

    return (
        <div className={className}>
            {children}
        </div>
    );
}