
    // project data table
    $(document).ready(function() {
        $('#projectTable')
        .addClass( 'nowrap' )
        .dataTable( {
            responsive: true,
            columnDefs: [
                { targets: [-1, -3], className: 'dt-body-right' }
            ]
        });
    });
    // employees Line Column
    $(document).ready(function() {
        var options = {
            chart: {
                height: 350,
                type: 'line',
                toolbar: {
                    show: false,
                },
            },
            colors: ['var(--chart-color1)', 'var(--chart-color2)'],
            series: [{
                name: 'Working Hours',
                type: 'column',
                data: [440, 505, 414, 671, 227, 413, 201, 352, 752, 320, 257, 160]
            }, {
                name: 'Employees Progress',
                type: 'line',
                data: [23, 42, 35, 27, 43, 22, 17, 31, 22, 22, 12, 16]
            }],
            stroke: {
                width: [0, 4]
            },        
             //labels: ["Jan", "Feb", "Mar", "Apr", "May", "Jun", "Jul", "Aug", "Sep", "Oct", "Nov", "Dec"],    
            labels: ['2001', '2002', '2003', '2004', '2005', '2006', '2007', '2008', '2009', '2010', '2011', '2012'],
            xaxis: {
                type: 'datetime'
            },
            yaxis: [{
                title: {
                    text: 'Working Hours',
                },

            }, {
                opposite: true,
                title: {
                    text: 'Employees Progress'
                }
            }]
        }
        var chart = new ApexCharts(
            document.querySelector("#apex-chart-line-column"),
            options
        );

        chart.render();
    });

    // employees circle
    $(document).ready(function() {
        var options = {
            chart: {
                height: 250,
                type: 'radialBar',
            },
            colors: ['var(--chart-color1)'],
            plotOptions: {
                radialBar: {
                    hollow: {
                        size: '70%',
                    }
                },
            },
            series: [70],
            labels: ['Working'],
        }
        var chart = new ApexCharts(
            document.querySelector("#apex-circle-chart"),
            options
        );

        chart.render();
    });

