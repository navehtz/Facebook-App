using FacebookMini.ui.CustomComponent;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using FacebookMini.shared.adapters;

namespace FacebookMini.ui.PageBuilder
{
    public class FeedPageBuilder : IPageBuilder
    {
        private readonly PageBuildContext r_Context;

        private Panel m_FeedPanel;
        private Label m_HeaderLabel;
        private FlowLayoutPanel m_PostsFlowPanel;

        public FeedPageBuilder(PageBuildContext i_Context)
        {
            r_Context = i_Context;
        }

        public void Reset()
        {
            m_FeedPanel = new Panel { Dock = DockStyle.Fill };
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
        }

        public void BindData()
        {
            IEnumerable<IPostData> feedPosts = r_Context.AppLogic.GetFriendsFeedPostsData();

            foreach (IPostData postData in feedPosts)
            {
                PostComponent postControl = new PostComponent
                {
                    Margin = new Padding(5, 5, 5, 15),
                    AppLogic = r_Context.AppLogic
                };

                postControl.SetPost(postData);
                m_PostsFlowPanel.Controls.Add(postControl);
            }
        }

        public Control GetResult()
        {
            return m_FeedPanel;
        }
    }
}
