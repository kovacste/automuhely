export function exportToCsv(data) {
    let csv = "data:text/csv;charset=utf-8,";
    const array = [Object.keys(data[0])].concat(data)
    return csv +  array.map(item => {
        return Object.values(item).toString()
    }).join('\n')
}