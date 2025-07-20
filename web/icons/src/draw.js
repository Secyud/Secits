draw(cross(256, 32));

function draw(arr) {
    let str = 'd="';
    let first = true;
    for (const value of arr) {
        str += first ? `M${value[0]} ${value[1]}` : `L${value[0]} ${value[1]}`;
        first = false;
    }
    str += 'z"'
    console.log(str);
}

function cross(t, m) {
    return [
        [m, 3 * m],
        [3 * m, m],
        [t, t - 2 * m],
        [2 * t - 3 * m, m],
        [2 * t - m, 3 * m],
        [t + 2 * m, t],
        [2 * t - m, 2 * t - 3 * m],
        [2 * t - 3 * m, 2 * t - m],
        [t, t + 2 * m],
        [3 * m, 2 * t - m],
        [m, 2 * t - 3 * m],
        [t - 2 * m, t]
    ];
}