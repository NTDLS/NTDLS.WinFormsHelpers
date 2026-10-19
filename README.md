# NTDLS.WinFormsHelpers

**NTDLS.WinFormsHelpers** is a small NuGet package of helper utilities for Windows Forms applications. It provides thread-safe extension methods for `Control`, `Form` and `ListView` that remove the boilerplate around cross-thread `Invoke` calls, validated value accessors for `TextBox` controls, tooltip helpers, a spinning `ActivityIndicator` user control, and a ready-made `ProgressForm` for running work on a background thread while reporting progress to the user.

📦 Be sure to check out the NuGet package: https://www.nuget.org/packages/NTDLS.WinFormsHelpers

## When to use this package

Use **NTDLS.WinFormsHelpers** when you are writing a WinForms app and keep running into one or more of these problems:

- **Cross-thread exceptions.** Updating a label, list view, or dialog from a worker thread throws `InvalidOperationException`. The `Invoke*` extension methods handle `InvokeRequired` for you, so the same call works whether it runs on the UI thread or not.
- **Tedious input validation.** You want a typed value out of a `TextBox` with range or length checks, and a clear error message when the user enters something invalid.
- **Long-running operations.** You need to run work off the UI thread while showing a progress form — including optional cancellation — without hand-rolling threads, `Invoke`s, and synchronization.
- **Polish.** You want tooltips applied in bulk (with automatic text wrapping) or a lightweight spinning activity indicator.

If your app is pure console/server code, WPF, or Blazor, this package will not help you — it is strictly WinForms.

## Usage examples

### Thread-safe control updates

```csharp
using NTDLS.WinFormsHelpers; // required for the extension methods

// Safe to call from any thread — no InvokeRequired check needed.
labelStatus.InvokeSetText("Working...");
buttonSave.InvokeDisable();          // disable while work is in flight
buttonSave.InvokeEnable();           // re-enable when done

// Close a dialog from a worker thread.
this.InvokeClose(DialogResult.OK);

// Show a message box from any thread; returns the button the user pressed.
DialogResult result = this.InvokeMessageBox("Done!", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
```

### ListView updates from another thread

```csharp
using NTDLS.WinFormsHelpers;

// All of these are safe to call from a background thread.
listView.InvokeClearRows();                       // clear all items
listView.Items.Add(new ListViewItem("Item"));     // then populate...
listView.InvokeDeleteItem(listView.Items[0]);     // remove one item
listView.InvokeDeleteSelectedItems();             // remove everything selected
listView.InvokeClearSelection();                  // deselect all
```

### Sorting a ListView by column

```csharp
using NetTunnel.UI.Helpers; // note: the comparer lives in this namespace

var comparer = new ListViewItemComparer { SortColumn = 1, SortOrder = SortOrder.Ascending };
listView.ListViewItemSorter = comparer;
listView.Sort(); // rows now sorted by column 1, ascending
```

### Reading and validating TextBox values

```csharp
using NTDLS.WinFormsHelpers;

// Parse and range-check an integer ([min]/[max] are substituted into the message).
int port = txtPort.GetAndValidateNumeric(1, 65535, "Port must be between [min] and [max].");

// Require non-empty text; throws with your message if the box is blank.
string name = txtName.GetAndValidateText("A name is required.");

// Convert the text to any type supported by the underlying converter.
decimal price = txtPrice.ValueAs<decimal>();
```

All validators throw an `Exception` carrying your message when the value is missing or out of range.

### Tooltips

```csharp
using NTDLS.WinFormsHelpers;

// Create a tooltip owned by the form — it disposes itself when the form closes.
ToolTip tips = this.CreateToolTipControl();

// Apply the same (soft-wrapped) text to several controls at once.
tips.AddControls(new Control[] { btnOpen, btnSave, btnCancel },
                 "Performs the action after validating all inputs.", wrapLength: 40);
```

### Dark mode

WinForms (.NET 9 and later) can follow the Windows light/dark setting, but a few controls are left half done. `Theming` does the setup and the
fixes in one place:

```csharp
using NTDLS.WinFormsHelpers;

// Once, before any form is created (Program.cs):
Theming.InitializeApplication();

// In the constructor of every form, after InitializeComponent():
Theming.InitializeForm(this);
```

`InitializeForm` covers the form, everything on it, and controls added to it later. In dark mode it fixes:

* **Link labels**, which would be dark blue on a dark background.
* **Tab pages**, which keep a light background.
* **Combo boxes** on the first tab, which would stay white (WinForms only themes a combo box whose handle is created after the form).
* **Data grids**: the header cells, which stay white, and **link columns**, whose links would be dark blue (also for columns added later).

In light mode it does nothing. For the few things you paint yourself, `Theming` has colors that suit the current mode:

```csharp
row.BackColor = Theming.StatusErrorBack;      // also StatusWarningBack and StatusGoodBack
row.ForeColor = Theming.StatusText;           // the text color for those backgrounds
label.ForeColor = Theming.Muted;              // secondary text
rowHighlight.BackColor = Theming.Highlight;
bool dark = Theming.IsDarkMode;
```

### ProgressForm for background work

```csharp
using NTDLS.WinFormsHelpers;

// Runs the worker on a background thread, shows the progress form,
// and closes it automatically when the worker finishes.
var progress = new ProgressForm("Exporting", "Please wait", "Building the report...");
progress.Execute(sender =>
{
    for (int i = 0; i < 100; i++)
    {
        DoWork(i);
        sender.SetProgressValue(i + 1);   // thread-safe progress update
        if (sender.IsCancelPending) break; // honor the Cancel button
    }
});
progress.Dispose();
```

There are async variants too (`ExecuteAsync`) and overloads that return a result:

```csharp
// Fire-and-forget async work.
await progress.ExecuteAsync(async sender =>
{
    await Task.Delay(2000);
});

// Async work that returns a value.
string result = await progress.ExecuteAsync<string>(async sender =>
{
    return await GenerateReportAsync(sender);
});
```

You can also drive the form manually via `ShowDialog(...)`, `SetHeaderText`, `SetBodyText`, `IncrementProgressValue`, `SetCanCancel`, and the `OnCancel` event.

### ActivityIndicator control

```csharp
using NTDLS.WinFormsHelpers.Controls;

var spinner = new ActivityIndicator
{
    AutoIncrement = true,            // rotate automatically
    AutoIncrementFrequency = 80      // milliseconds per step
};
spinner.ActiveSegmentColor = Color.SteelBlue;
form.Controls.Add(spinner);
```

## License

[MIT](https://choosealicense.com/licenses/mit/)
