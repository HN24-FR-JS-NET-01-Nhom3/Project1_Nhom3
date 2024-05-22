function renderAdminCharts(lotteries, purchases, searches, categories) {
  // Tìm giá trị lớn nhất trong dữ liệu
  const maxLottery = Math.max(...lotteries);
  const maxPurchase = Math.max(...purchases);
  const maxSearch = Math.max(...searches);
  const maxDataValue = Math.max(maxLottery, maxPurchase, maxSearch);

  // Làm tròn giá trị lớn nhất lên bội số gần nhất của 100
  const maxY = Math.ceil(maxDataValue / 100) * 100;

  // =====================================
  // Profit
  // =====================================
  var chartOptions = {
    series: [
      { name: "Số giải đã phát hành", data: lotteries },
      { name: "Số vé đã bán", data: purchases },
      { name: "Số vé đã tìm kiếm", data: searches },
    ],

    chart: {
      type: "bar",
      height: 400,
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

$(document).ready(function () {
  $('#selectNumberOfMonths').change(function () {
    var selectedValue = $(this).val();
    $.ajax({
      type: 'GET',
      url: '/admin/get-admin-statistics-ajax', // Địa chỉ của action trong Controller MVC
      data: { numberOfMonths: selectedValue },
      success: function (response) {
        console.log(response.result);
        var monthlyStatistic = response.result[0].monthlyStatistic;
        var lotteries = monthlyStatistic.map(stat => stat.lotteryCount);
        var purchases = monthlyStatistic.map(stat => stat.purchaseCount);
        var searches = monthlyStatistic.map(stat => stat.searchCount);
        var categories = monthlyStatistic.map(stat => `${stat.month}/${stat.year}`);

        // Vẽ lại biểu đồ với dữ liệu mới
        $("#chart").empty();
        renderAdminCharts(lotteries, purchases, searches, categories);
      },
      error: function (xhr, status, error) {
        console.error(error);
      }
    });
  });
});

