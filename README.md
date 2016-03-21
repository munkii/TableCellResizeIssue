# TableCellResizeIssue
1. To exercise the project load it up in a Simulator or device
2. Once the first View loads press "PRESS THIS FOR REPRO"
3. The SearchViewModel has been edited specifically for this repro and does not perform a real search. It is hardcoded to always return the same results

 
The CustomerItemCell uses a custom control derived from MvxView that stacks UILabels using StringStackPanel. For that stacking to work it needa the last item in the group to have a Bottom constraint. The current implementation of VerticalStackPanelConstraints in FluentLayout will only set a BottomConstraint if the parent is ScrollView.

This seems wrong to me. The StringStackPanel has three options for laying out the Labels see lines 95,96 and 97 of CreateStack in StringStackPanel.cs. 

* LayoutManually() lays out using FluentLayout without VerticalStackPanelConstraints
* LayoutWithFluentCopy() is a copy of the VerticalStackPanelConstraints implementation with 'if (parentView is UIScrollView)' commented out
* VerticalStackPanelConstraints simply calls the standard implementation





  
  
