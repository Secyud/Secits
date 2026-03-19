export class math {
    static valueEquals(a, b, t) {
        return a <= b + t && a >= b - t;
    }

    static rectContains(rect, point, t) {
        return this.valueEquals(point.x, (rect.left + rect.right) / 2, rect.width / 2 + t) &&
            this.valueEquals(point.y, (rect.top + rect.bottom) / 2, rect.height / 2 + t)
    }

    static rectEquals(rect1, rect2, t) {
        if (rect1 && rect2)
            return this.valueEquals(rect1.left, rect2.left, t) &&
                this.valueEquals(rect1.top, rect2.top, t) &&
                this.valueEquals(rect1.right, rect2.right, t) &&
                this.valueEquals(rect1.bottom, rect2.bottom, t)
        else return rect1 === rect2;
    }
}