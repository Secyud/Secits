export class math {
    static valueEquals(a, b, t) {
        return a <= b + t && a >= b - t;
    }

    static rectContains(rect, point, th, tv) {
        return this.valueEquals(point.x * 2, rect.left + rect.right, rect.width + th * 2) &&
            this.valueEquals(point.y * 2, rect.top + rect.bottom, rect.height + tv * 2)
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