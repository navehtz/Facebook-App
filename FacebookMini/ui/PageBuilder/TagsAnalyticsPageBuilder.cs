using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace FacebookMini.ui.PageBuilder
{
    public class TagsAnalyticsPageBuilder : IPageBuilder
    {
        private readonly PageBuildContext r_Context;

        private Panel m_MainPanel;
        private Label m_HeaderLabel;
        private Label m_InfoLabel;
        private Chart m_TagsChart;

        public TagsAnalyticsPageBuilder(PageBuildContext i_Context)
        {
            r_Context = i_Context;
        }

        public void Reset()
        {
            m_MainPanel = new Panel { Dock = DockStyle.Fill };
        }

        public void BuildHeader()
        {
            m_HeaderLabel = new Label
            {
                Text = "Tags Analytics",
                Dock = DockStyle.Top,
                Height = 40,
                Font = new Font("Segoe UI", 16F, FontStyle.Bold),
                Padding = new Padding(10, 5, 0, 0)
            };

            m_MainPanel.Controls.Add(m_HeaderLabel);
        }

        public void BuildBody()
        {
            m_InfoLabel = new Label
            {
                Dock = DockStyle.Top,
                Height = 30,
                Font = new Font("Segoe UI", 9F, FontStyle.Regular),
                Padding = new Padding(10, 5, 0, 0)
            };

            m_MainPanel.Controls.Add(m_InfoLabel);
            m_MainPanel.Controls.SetChildIndex(m_InfoLabel, 1);

            m_TagsChart = new Chart
            {
                Width = 500,
                Height = 350,
                Anchor = AnchorStyles.Top,
                Top = 80
            };

            m_TagsChart.Left = (m_MainPanel.Width - m_TagsChart.Width) / 2;
            m_MainPanel.Resize += delegate
            {
                m_TagsChart.Left = (m_MainPanel.Width - m_TagsChart.Width) / 2;
            };

            ChartArea chartArea = new ChartArea("TagsArea");
            m_TagsChart.ChartAreas.Add(chartArea);

            Series series = new Series("Tags")
            {
                ChartType = SeriesChartType.Pie,
                YValueType = ChartValueType.Int32,
                IsValueShownAsLabel = true,
                Label = "#AXISLABEL (#PERCENT{P0})"
            };
            series["PieLabelStyle"] = "Outside";
            series["PieLineColor"] = "Black";

            m_TagsChart.Series.Add(series);

            m_MainPanel.Controls.Add(m_TagsChart);
            m_MainPanel.Controls.SetChildIndex(m_TagsChart, 2);
        }

        public void BindData()
        {
            Series series = m_TagsChart.Series[0];
            series.Points.Clear();

            ICollection<string> allTags = r_Context.AppLogic.GetAllTags();

            Dictionary<string, int> tagsCountDictionary =
                new Dictionary<string, int>(StringComparer.OrdinalIgnoreCase);
            int total = 0;

            if (allTags != null)
            {
                foreach (string tag in allTags)
                {
                    if (!string.IsNullOrEmpty(tag))
                    {
                        tagsCountDictionary[tag] = tagsCountDictionary.TryGetValue(tag, out int c) ? c + 1 : 1;
                        total++;
                    }
                }
            }

            if (total == 0)
            {
                m_InfoLabel.Text = "No tags to display yet. Add tags to your posts first.";
                return;
            }

            m_InfoLabel.Text = "Showing distribution of all tags by percentage.";

            foreach (KeyValuePair<string, int> pair in tagsCountDictionary)
            {
                DataPoint point = new DataPoint();
                point.YValues = new double[] { pair.Value };
                point.AxisLabel = pair.Key;
                series.Points.Add(point);
            }
        }

        public Control GetResult()
        {
            return m_MainPanel;
        }
    }
}
