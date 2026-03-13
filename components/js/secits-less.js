function getSize(d) {
    if (typeof d !== 'number') {
        d = d.value;
    }
    if (d === 0) {
        return "0";
    }
    return `calc(var(--s-s) / ${d})`;
}


module.exports = {
    install: function (less, pluginManager, functions) {
        function getRuleset(name, value) {
            return new less.tree.Ruleset([], [
                new less.tree.Declaration(name, value)
            ]);
        }

        functions.add('get-item', function (target, items) {
            let res = "default";
            if (typeof items.value === 'string') {
                if (target.value === items.value) {
                    res = target.value;
                }
            } else {
                for (let item of items.value) {
                    if (target.value === item.value) {
                        res = target.value;
                        break;
                    }
                }
            }

            return res;
        });

        /**
         * 获取关于--s-s的计算值
         * */
        functions.add('get-size', getSize);

        functions.add("size-trbl", function (t, r, b, l) {
            return `${getSize(t)} ${getSize(r)} ${getSize(b)} ${getSize(l)}`;
        })
        functions.add("size-vh", function (v, h) {
            return `${getSize(v)} ${getSize(h)}`;
        })
        functions.add("size-a", function (v) {
            return `${getSize(v)}`;
        })
        functions.add("default-size", function () {
            return `${getSize(4)} ${getSize(2)}`;
        })
    }
};