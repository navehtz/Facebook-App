using FacebookMini.ui.CustomComponent;
using FacebookWrapper.ObjectModel;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using FacebookMini.ui.Adapters;

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
            if (r_Context.LoggedInUser.Friends == null || r_Context.LoggedInUser.Friends.Count == 0)
            {
                return; 
            }

            HashSet<KeyValuePair<Post, User>> friendsPosts = new HashSet<KeyValuePair<Post, User>>();

            foreach (User friend in r_Context.LoggedInUser.Friends)
            {
                if (friend?.Posts == null) continue;

                foreach (Post post in friend.Posts)
                {
                    if (post == null) continue;
                    friendsPosts.Add(new KeyValuePair<Post, User>(post, friend));
                }
            }

            foreach (KeyValuePair<Post, User> postOfFriend in friendsPosts)
            {
                var postControl = new PostComponent
                {
                    Margin = new Padding(5, 5, 5, 15),
                    PostNotesManager = r_Context.NotesManager,
                    PostTagsManager = r_Context.TagsManager
                };

                IPostData postData = new FacebookPostAdapter(postOfFriend.Key, postOfFriend.Value);
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
