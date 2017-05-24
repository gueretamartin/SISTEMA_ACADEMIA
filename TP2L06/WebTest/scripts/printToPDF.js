function imprimirReporte(nombre) {
    var pdf = new jsPDF('p', 'pt', 'letter');
    source = $('#pdfPrinter')[0];
    specialElementHandlers = {
        '#table': function (element, renderer) {
            return true
        }
    };
    margins = {
        top: 80,
        bottom: 60,
        left: 40,
        width: 522
    };
    pdf.fromHTML(
        source,
        margins.left,
        margins.top, {
            'width': margins.width,
            'elementHandlers': specialElementHandlers
        },
        function (dispose) {
            pdf.save(nombre + "_" + new Date().toLocaleDateString() + "_" + new Date().toLocaleTimeString() + '.pdf');
        }
        , margins);
}
