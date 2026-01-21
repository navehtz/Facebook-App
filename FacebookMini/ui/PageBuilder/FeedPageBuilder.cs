using System.Drawing;
using System.Windows.Forms;

namespace FacebookMini.ui.PageBuilder
{
    public class FeedPageBuilder : IPageBuilder
    {
        private PageBuildContext m_Context;

        private Panel m_FeedPanel;
        private Label m_HeaderLabel;
        private FlowLayoutPanel m_PostsFlowPanel;

        public void Reset()
        {
            m_FeedPanel = new Panel { Dock = DockStyle.Fill };
        }

        public void DeliverContext(PageBuildContext i_Context) 
        {
            m_Context = i_Context;
        }

        public void BuildHeader()
        {
            m_HeaderLabel = new Label
            {
                Text = "Feed",
                Dock = DockStyle.Top,
                Height = 45,
                Font = new Font("Segoe UI", 16F, FontStyle.Bold),
                Padding = new Padding(10, 5, 0, 5)
            };

            m_FeedPanel.Controls.Add(m_HeaderLabel);
            m_HeaderLabel.Name = "feedHeaderLabel";
        }

        public void BuildBody()
        {
            m_PostsFlowPanel = new FlowLayoutPanel
            {
                Dock = DockStyle.Fill,
                AutoScroll = true,
                FlowDirection = FlowDirection.TopDown,
                WrapContents = false,
                Padding = new Padding(10, 5, 10, 10)
            };

            m_FeedPanel.Controls.Add(m_PostsFlowPanel);
            m_FeedPanel.Controls.SetChildIndex(m_PostsFlowPanel, 0);

            m_PostsFlowPanel.Name = "feedPostsFlow";
        }

        public Control GetResult()
        {
            return m_FeedPanel;
        }
    }
}
