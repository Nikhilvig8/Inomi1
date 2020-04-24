//== Class definition
var Dashboard = (function() {
  //== Profit Share Chart.
  var profitShare = function() {
    if ($("#m_chart_profit_share").length == 0) {
      return;
      }

      var TotalTask = 25;
      var TotalEssay = 150;
      var TotalMassege = 25;

    var chart = new Chartist.Pie(
      "#m_chart_profit_share",
      {
        series: [
          {
                value: TotalTask,
            className: "custom",
            meta: {
              color: mUtil.getColor("brand")
            }
          },
          {
              value: TotalEssay,
            className: "custom",
            meta: {
              color: mUtil.getColor("accent")
            }
          },
          {
              value: TotalMassege,
            className: "custom",
            meta: {
              color: mUtil.getColor("warning")
            }
          }
        ],
        labels: [1, 2, 3]
      },
      {
        donut: true,
        donutWidth: 13,
        showLabel: false
      }
    );

    chart.on("draw", function(data) {
      if (data.type === "slice") {
        // Get the total path length in order to use for dash array animation
        var pathLength = data.element._node.getTotalLength();

        // Set a dasharray that matches the path length as prerequisite to animate dashoffset
        data.element.attr({
          "stroke-dasharray": pathLength + "px " + pathLength + "px"
        });

        // Create animation definition while also assigning an ID to the animation for later sync usage
        var animationDefinition = {
          "stroke-dashoffset": {
            id: "anim" + data.index,
            dur: 1000,
            from: -pathLength + "px",
            to: "0px",
            easing: Chartist.Svg.Easing.easeOutQuint,
            // We need to use `fill: 'freeze'` otherwise our animation will fall back to initial (not visible)
            fill: "freeze",
            stroke: data.meta.color
          }
        };

        // If this was not the first slice, we need to time the animation so that it uses the end sync event of the previous animation
        if (data.index !== 0) {
          animationDefinition["stroke-dashoffset"].begin =
            "anim" + (data.index - 1) + ".end";
        }

        // We need to set an initial value before the animation starts as we are not in guided mode which would do that for us

        data.element.attr({
          "stroke-dashoffset": -pathLength + "px",
          stroke: data.meta.color
        });

        // We can't use guided mode as the animations need to rely on setting begin manually
        // See http://gionkunz.github.io/chartist-js/api-documentation.html#chartistsvg-function-animate
        data.element.animate(animationDefinition, false);
      }
    });

    // For the sake of the example we update the chart every time it's created with a delay of 8 seconds
    chart.on("created", function() {
      if (window.__anim21278907124) {
        clearTimeout(window.__anim21278907124);
        window.__anim21278907124 = null;
      }
      window.__anim21278907124 = setTimeout(chart.update.bind(chart), 15000);
    });
  };

  return {
    //== Init demos
    init: function() {
      // init charts
      profitShare();
    }
  };
})();

//== Class initialization on page load
jQuery(document).ready(function() {
  Dashboard.init();
});
