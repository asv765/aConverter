using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using aConverterClassLibrary;

namespace aConverter.Forms
{
    public partial class FormCheckCasesListExtended : Form
    {
        private bool _cancelAction;

        private List<CheckCase> _checkCaseList = new List<CheckCase>();

        public FormCheckCasesListExtended()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void FormCheckCasesListExtended_Load(object sender, EventArgs e)
        {
            Text = @"Варианты проверки (" + aConverter_RootSettings.FirebirdStringConnection + @")";
            _checkCaseList = CheckCaseFactory.GenerateCheckCases();

            _checkCaseList.Select(GenerateTreeNode).ToList();
            
            var forSubNodes = GenerateTreeNode(null);
            var tnRoot = new TreeNode("Варианты проверки", 5, 5, forSubNodes.Nodes.Cast<TreeNode>().ToArray());

            treeViewMain.Nodes.Add(tnRoot);
            treeViewMain.ExpandAll();
        }

        private TreeNode GenerateTreeNode(CheckCase checkCase)
        {
            var treeNodeChildList = new List<TreeNode>();
            foreach (CheckCase localCheckCase in _checkCaseList)
            {
                if (localCheckCase.DependOn == checkCase)
                {
                    var rtr = GenerateTreeNode(localCheckCase);
                    treeNodeChildList.Add(rtr);
                }
            }
            string nodeText = checkCase == null ? "Варианты проверки" : checkCase.Description;
            var treeNode = new TreeNode(nodeText, 3, 3, treeNodeChildList.ToArray()) { Tag = checkCase };
            return treeNode;
        }

        private void treeViewMain_DoubleClick(object sender, EventArgs e)
        {
            if (treeViewMain.SelectedNode != null)
            {
                Test(treeViewMain.SelectedNode, false);
            }
        }

        private void Test(TreeNode treeNode, bool withSubNodes)
        {
            var treeNodeStatus = (TreeNodeStatus) treeNode.ImageIndex;
            var checkCase = (CheckCase)treeNode.Tag;

            if (withSubNodes)
            {
                if (treeNodeStatus != TreeNodeStatus.Пауза && treeNodeStatus != TreeNodeStatus.ПроверенВсеOK)
                {
                    if (checkCase.Test())
                    {
                        MarkChildNodes(treeNode, TreeNodeStatus.НеПроверенИзЗаОшибкиВерхнегоУзла);
                        MarkNode(treeNode,
                            checkCase.CanFix
                                ? TreeNodeStatus.ПроверенОшибкаМожноИсправить
                                : TreeNodeStatus.ПроверенОшибка);
                        return;
                    }
                    MarkNode(treeNode, TreeNodeStatus.ПроверенВсеOK);
                }
                foreach (TreeNode treeNodeChild in treeNode.Nodes) Test(treeNodeChild, true);
            }
            else // Тестируется только текущий узел
            {
                if (checkCase.Test())
                {
                    MarkNode(treeNode,
                        checkCase.CanFix
                            ? TreeNodeStatus.ПроверенОшибкаМожноИсправить
                            : TreeNodeStatus.ПроверенОшибка);
                    DataTable dt = checkCase.Analize();
                    var fdt = new FormDataTable(checkCase.AnalyzeResultDescription, dt);
                    fdt.ShowDialog();
                }
                else
                    MarkNode(treeNode, TreeNodeStatus.ПроверенВсеOK);
            }
        }

        private void MarkChildNodes(TreeNode treeNode, TreeNodeStatus treeNodeStatus)
        {
            if (treeNode != null)
            {
                MarkNode(treeNode, treeNodeStatus);
                foreach (TreeNode treeNodeChild in treeNode.Nodes) MarkChildNodes(treeNodeChild, treeNodeStatus);
            }
        }

        private void MarkNode(TreeNode treeNode, TreeNodeStatus treeNodeStatus)
        {
            if (treeNode != null)
            {
                treeNode.ImageIndex = (int) treeNodeStatus;
                treeNode.SelectedImageIndex = (int) treeNodeStatus;
                Application.DoEvents(); // Для отрисовки изменений
            }
        }

        private void toolStripButtonGo_Click(object sender, EventArgs e)
        {
            foreach (TreeNode treeNodeChild in treeViewMain.Nodes[0].Nodes) Test(treeNodeChild, true);
        }

        private void toolStripButtonSetGo_Click(object sender, EventArgs e)
        {
            MarkChildNodes(treeViewMain.Nodes[0], TreeNodeStatus.ТребуетПроверки);
            MarkNode(treeViewMain.Nodes[0], TreeNodeStatus.КорнневойУзел);
        }

        private void toolStripButtonPause_Click(object sender, EventArgs e)
        {
            MarkChildNodes(treeViewMain.Nodes[0], TreeNodeStatus.Пауза);
            MarkNode(treeViewMain.Nodes[0], TreeNodeStatus.КорнневойУзел);
        }

        /// <summary>
        /// Метод чтобы не свертывался узел дерева при двойном клике мышью
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void treeViewMain_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Left)
                return;
            if (e.Clicks != 2)
                return;
            TreeNode node = treeViewMain.GetNodeAt(e.Location);
            if (node == null)
                return;
            if (!node.Bounds.Contains(e.Location))
                return;
            if (node != treeViewMain.SelectedNode)
                return;
            _cancelAction = true;
        }

        private void treeViewMain_BeforeCollapse(object sender, TreeViewCancelEventArgs e)
        {
            if (_cancelAction)
            {
                e.Cancel = true;
                _cancelAction = false;
            }
        }

        private void treeViewMain_BeforeExpand(object sender, TreeViewCancelEventArgs e)
        {
            if (_cancelAction)
            {
                e.Cancel = true;
                _cancelAction = false;
            }
        }

        private void toolStripButtonFix_Click(object sender, EventArgs e)
        {
            foreach (TreeNode treeNodeChild in treeViewMain.Nodes[0].Nodes) Fix(treeNodeChild, true);
        }

        private void Fix(TreeNode treeNode, bool withSubNodes)
        {
            var treeNodeStatus = (TreeNodeStatus)treeNode.ImageIndex;
            var checkCase = (CheckCase)treeNode.Tag;

            if (treeNodeStatus == TreeNodeStatus.ПроверенОшибкаМожноИсправить ||
                (!withSubNodes && checkCase.CanFix))
            {
                checkCase.Fix();
                Test(treeNode, false); // Возможно, что после неудачной попытки исправления появится статистика с ошибками. Ну и пусть.
            }
            if (withSubNodes)
            {
                foreach (TreeNode treeNodeChild in treeNode.Nodes) Fix(treeNodeChild, true);
            }
            if (!withSubNodes && !checkCase.CanFix)
                MessageBox.Show(@"Варианта исправления не предусмотрено", @"Внимание", MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
        }

        private void тестироватьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (treeViewMain.SelectedNode != null) Test(treeViewMain.SelectedNode, false);
        }

        private void исправитьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (treeViewMain.SelectedNode != null) Fix(treeViewMain.SelectedNode, false);
        }

        private void пометитьКТестированиюToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (treeViewMain.SelectedNode != null) MarkNode(treeViewMain.SelectedNode, TreeNodeStatus.ТребуетПроверки);
        }

        private void исключитьИзТестированияToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (treeViewMain.SelectedNode != null) MarkNode(treeViewMain.SelectedNode, TreeNodeStatus.Пауза);
        }
    }

    enum TreeNodeStatus
    {
        ПроверенВсеOK,
        ПроверенОшибка,
        Пауза,
        ТребуетПроверки,
        НеПроверенИзЗаОшибкиВерхнегоУзла,
        КорнневойУзел,
        ПроверенОшибкаМожноИсправить
    }
}
