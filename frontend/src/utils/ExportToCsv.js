export function exportToCsv(data) {
    let csv = "data:text/csv;charset=utf-8,";
    let arrLen = 0;
    let i = 0;
    data.forEach((arr, index) => {
        if(Object.keys(arr).length > arrLen) {
            arrLen = Object.keys(arr).length;
            i = index;
        }
    });
    const array = [Object.keys(data[i])].concat(data)
    return csv +  array.map(item => {
        return Object.values(item).toString()
    }).join('\n')
}
