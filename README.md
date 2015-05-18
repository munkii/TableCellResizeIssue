# TableCellResizeIssue
Small MVVMCross iOS project to support this Stack Overflow question, **"UILabel not wrapping in UITableView until device rotate (iOS8)"** http://stackoverflow.com/questions/30099363/uilabel-not-wrapping-in-uitableview-until-device-rotate-ios8

1. To exercise the project load it up in a Simulator or device
2. Once the first View loads press "Search"
3. The SearchViewModel has been edited specifically for this repro and does not perform a real search. It is hardcoded to support 3 search strings

  3.1 "smi" will return a couple of screens worth of results. All the items for without a need to wrap
  
  3.2 "thi" will return 4 results one of them comntain a customer record with a very long name that will causes wrapping
  
  3.3 "this" will return 1 result, the same customer record as 3.2
  
  
  
