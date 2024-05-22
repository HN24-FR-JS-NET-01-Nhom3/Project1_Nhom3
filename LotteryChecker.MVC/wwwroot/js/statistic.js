function renderCharts(purchaseData, winData, categories) {
    // Tìm giá trị lớn nhất trong dữ liệu
    const maxPurchase = Math.max(...purchaseData);
    const maxWin = Math.max(...winData);
    const maxDataValue = Math.max(maxPurchase, maxWin);

    // Làm tròn giá trị lớn nhất lên bội số gần nhất của 100
    const maxY = Math.ceil(maxDataValue / 100) * 100;

    // =====================================
    // Profit
    // =====================================
    var chartOptions = {
        series: [
            { name: "Số vé mua", data: purchaseData },
            { name: "Số vé trúng", data: winData },
        ],

        chart: {
            type: "bar",
            height: 345,
            offsetX: -15,
            toolbar: { show: true },
            foreColor: "#adb0bb",
            fontFamily: 'inherit',
            sparkline: { enabled: false },
        },

        colors: ["#5D87FF", "#49BEFF"],

        plotOptions: {
            bar: {
                horizontal: false,
                columnWidth: "35%",
                borderRadius: [6],
                borderRadiusApplication: 'end',
                borderRadiusWhenStacked: 'all'
            },
        },
        markers: { size: 0 },

        dataLabels: {
            enabled: false,
        },

        legend: {
            show: false,
        },

        grid: {
            borderColor: "rgba(0,0,0,0.1)",
            strokeDashArray: 3,
            xaxis: {
                lines: {
                    show: false,
                },
            },
        },

        xaxis: {
            type: "category",
            categories: categories,
            labels: {
                style: { cssClass: "grey--text lighten-2--text fill-color" },
            },
        },

        yaxis: {
            show: true,
            min: 0,
            max: maxY, // Sử dụng giá trị lớn nhất tìm được
            tickAmount: 4,
            labels: {
                style: {
                    cssClass: "grey--text lighten-2--text fill-color",
                },
            },
        },
        stroke: {
            show: true,
            width: 3,
            lineCap: "butt",
            colors: ["transparent"],
        },

        tooltip: { theme: "light" },

        responsive: [
            {
                breakpoint: 600,
                options: {
                    plotOptions: {
                        bar: {
                            borderRadius: 3,
                        }
                    },
                }
            }
        ]
    };

    var profitChart = new ApexCharts(document.querySelector("#chart"), chartOptions);
    profitChart.render();
}
