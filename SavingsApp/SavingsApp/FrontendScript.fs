namespace SavingsApp

open System
open WebSharper.Html
open WebSharper.JavaScript
open WebSharper.Html.Server

module SavingsUI =
 open SavingsApp

    // UI to display the list of persons and their savings
    let displayPersons () =
        Div [
            for person in SavingsLogic.appState.Persons do
                Div [
                    H4 [Text person.Name]
                    P [Text (sprintf "Savings Goal: %.2f %s" person.SavingsGoal person.Currency)]
                    P [Text (sprintf "Current Savings: %.2f %s" person.Savings person.Currency)]
                ]
        ]

    // UI for adding a person
    let addPersonForm () =
        Div [
            Input [Attr.Type "text"; Attr.Placeholder "Name"; Prop.Name "personName"]
            Input [Attr.Type "number"; Attr.Placeholder "Savings Goal"; Prop.Name "savingsGoal"]
            Input [Attr.Type "text"; Attr.Placeholder "Currency"; Prop.Name "currency"]
            Input [Attr.Type "button"; Attr.Value "Add"; OnClick (fun _ ->
                let name = doc?querySelector("[name=personName]").As<HTMLInputElement>().value
                let goal = float (doc?querySelector("[name=savingsGoal]").As<HTMLInputElement>().value)
                let currency = doc?querySelector("[name=currency]").As<HTMLInputElement>().value
                SavingsLogic.addPerson { Name = name; SavingsGoal = goal; Savings = 0.0; Currency = currency; ReminderFrequency = ""; NextReminderDate = DateTime.MinValue }
                window.alert("Person added successfully!") // Display confirmation pop-up
                )
            ]
        ]

    // UI for deleting a person
    let deletePersonForm () =
        Div [
            Input [Attr.Type "text"; Attr.Placeholder "Name"; Prop.Name "personName"]
            Input [Attr.Type "button"; Attr.Value "Delete"; OnClick (fun _ ->
                let name = doc?querySelector("[name=personName]").As<HTMLInputElement>().value
                SavingsLogic.deletePerson name
                window.alert("Person deleted successfully!") 
                )
            ]
        ]

    // UI for setting savings goal
    let setSavingsGoalForm () =
        Div [
            Input [Attr.Type "text"; Attr.Placeholder "Name"; Prop.Name "personName"]
            Input [Attr.Type "number"; Attr.Placeholder "New Savings Goal"; Prop.Name "newGoal"]
            Input [Attr.Type "button"; Attr.Value "Set Goal"; OnClick (fun _ ->
                let name = doc?querySelector("[name=personName]").As<HTMLInputElement>().value
                let goal = float (doc?querySelector("[name=newGoal]").As<HTMLInputElement>().value)
                SavingsLogic.setSavingsGoal name goal
                window.alert("Savings goal updated successfully!") 
                )
            ]
        ]

    // UI for adding savings
    let addSavingsForm () =
        Div [
            Input [Attr.Type "text"; Attr.Placeholder "Name"; Prop.Name "personName"]
            Input [Attr.Type "number"; Attr.Placeholder "Amount"; Prop.Name "amount"]
            Input [Attr.Type "button"; Attr.Value "Add Savings"; OnClick (fun _ ->
                let name = doc?querySelector("[name=personName]").As<HTMLInputElement>().value
                let amount = float (doc?querySelector("[name=amount]").As<HTMLInputElement>().value)
                SavingsLogic.addSavings name amount
                window.alert("Savings added successfully!") 
            ]
        ]

    // UI for exchanging currency
    let exchangeCurrencyForm () =
        Div [
            Input [Attr.Type "text"; Attr.Placeholder "Name"; Prop.Name "personName"]
            Input [Attr.Type "text"; Attr.Placeholder "New Currency"; Prop.Name "newCurrency"]
            Input [Attr.Type "button"; Attr.Value "Exchange"; OnClick (fun _ ->
                let name = doc?querySelector("[name=personName]").As<HTMLInputElement>().value
                let currency = doc?querySelector("[name=newCurrency]").As<HTMLInputElement>().value
                SavingsLogic.exchangeCurrency name currency
                window.alert("Currency exchanged successfully!") 
                )
            ]
        ]

    // UI for setting reminders
    let setReminderForm () =
        Div [
            Input [Attr.Type "text"; Attr.Placeholder "Name"; Prop.Name "personName"]
            Select [
                Option [Attr.Value "Daily"] [Text "Daily"]
                Option [Attr.Value "Weekly"] [Text "Weekly"]
                Option [Attr.Value "Monthly"] [Text "Monthly"]
            ]
            Input [Attr.Type "button"; Attr.Value "Set Reminder"; OnClick (fun _ ->
                let name = doc?querySelector("[name=personName]").As<HTMLInputElement>().value
                let frequency = doc?querySelector("[name=reminderFrequency]").As<HTMLSelectElement>().value
                let nextDate = DateTime.Now.AddDays(1.0)
                SavingsLogic.updateReminder name frequency nextDate
                window.alert("Reminder set successfully!") 
                )
            ]
        ]

    // Combine all UI components into a single view
    let main () =
        Div [
            addPersonForm ()
            deletePersonForm ()
            setSavingsGoalForm ()
            addSavingsForm ()
            exchangeCurrencyForm ()
            setReminderForm ()
            displayPersons ()
        ]
