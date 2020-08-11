# funcui_gridsplitter_issue
Avalonia FuncUI GridSplitter issue

- 4 Rows in a grid
  - Row 1 - Buttons
  - Row 2 - List with 20 Items
  - Row 3 - GridSplitter
  - Row 4 - List with 30 Items
  
GridSplitter works initially.

Programmatically change both lists - Grid Splitter doesn't work.

Use a button to toggle whether to render the grid splitter or not.
Click once, it renders a stackpanel instead. Click again and it will render the GridSplitter.

Now it works again, and will work no matter if the lists are changed again.

Example:

![image](https://github.com/sharp-fsh/funcui_gridsplitter_issue/blob/master/GridSplitterIssue.gif)




