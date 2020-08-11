namespace GridSplitterIssue


module GridSplitterIssue =
    open Avalonia.Controls
    open Avalonia.FuncUI.DSL
    open Avalonia.Layout
    open Avalonia.FuncUI.Components
    open Avalonia.Controls.Primitives
    
    type FixGridSplitIssue =
    | RenderGridSplitter
    | DoNotRenderGridSplitter

    type State = { 
        ListOneItems : string list 
        ListTwoItems : string list 
        FixGridSplitIssue : FixGridSplitIssue
        }

    let generateStrings total word = 
        [
            for i in 0..total-1 do
                sprintf "%s: %i" word (i+1)
        ]

    let rowDefinition1 = "35, 100*, 5, 400*"

    let listViewA1 = generateStrings 20 "List A V1" 
    let listViewB1 = generateStrings 30 "List B V1" 

    let listViewA2 = generateStrings 5 "List A V2" 
    let listViewB2 = generateStrings 15 "List B V2" 

    let init = { 
        ListOneItems = listViewA1
        ListTwoItems = listViewB1
        FixGridSplitIssue = RenderGridSplitter
    }

    type Msg = 
    | ChangeItems
    | ToggleFix

    let update (msg: Msg) (state: State) : State =
        match msg with
        | ChangeItems -> { state with ListOneItems = if state.ListOneItems = listViewA1 then listViewA2 else listViewA1
                                      ListTwoItems = if state.ListTwoItems = listViewB1 then listViewB2 else listViewB1}
        | ToggleFix -> { state with FixGridSplitIssue =  if state.FixGridSplitIssue = RenderGridSplitter then  DoNotRenderGridSplitter else RenderGridSplitter }
        
    
    let buttonPanel gridPosition state dispatch = 
        StackPanel.create [
            gridPosition
            StackPanel.orientation Orientation.Horizontal
            StackPanel.children [
                Button.create [
                    Button.content "Change Lists"
                    Button.width 120.
                    Button.height 35.
                    Button.onClick (fun _ -> dispatch ChangeItems)
                ]
                Button.create [
                    Button.content (if state.FixGridSplitIssue = RenderGridSplitter then "Click Twice To Fix (1 of 2)" else "Click Again To Fix (2 of 2)")
                    Button.width 120.
                    Button.height 35.
                    Button.onClick (fun _ -> dispatch ToggleFix)
                ]
            ]
        ]
        
    let genericListBox (items : string list) = 
        ListBox.create  [
                ListBox.dataItems items
                ListBox.itemTemplate (
                    DataTemplateView<string>.create (fun (text) ->
                        TextBlock.create [ TextBlock.text text ]
                    )
                )
            ]

    let verticalScrollerWithItems gridPostion items =
        ScrollViewer.create     [
            gridPostion
            ScrollViewer.verticalScrollBarVisibility ScrollBarVisibility.Auto
            ScrollViewer.content (             
               genericListBox items
            )
        ]

    let gridSplitterComponent gridPosition = 
        GridSplitter.create [
            gridPosition
            GridSplitter.horizontalAlignment HorizontalAlignment.Stretch 
        ]
        
    let view (state: State) (dispatch) =
        Grid.create [
            Grid.rowDefinitions rowDefinition1
            Grid.showGridLines true
            Grid.children [
                buttonPanel (Grid.row 0) state dispatch
                verticalScrollerWithItems (Grid.row 1) state.ListOneItems
                match state.FixGridSplitIssue with
                | RenderGridSplitter -> gridSplitterComponent (Grid.row 2)
                | DoNotRenderGridSplitter -> StackPanel.create [ (Grid.row 2) ]
                verticalScrollerWithItems (Grid.row 3) state.ListTwoItems
            ]
        ]