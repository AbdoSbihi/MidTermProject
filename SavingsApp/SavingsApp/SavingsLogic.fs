namespace SavingsApp

open System

// Define a person type
type Person = {
    Name: string
    SavingsGoal: float
    Savings: float
    Currency: string
    ReminderFrequency: string
    NextReminderDate: DateTime
}

// Define the application state
type AppState = {
    mutable Persons: Person list
}

// Initialize the application state
let appState = { Persons = [] }

// Functions for modifying the state
let addPerson (person: Person) =
    appState.Persons <- person :: appState.Persons

let deletePerson (personName: string) =
    appState.Persons <- appState.Persons |> List.filter (fun p -> p.Name <> personName)

let setSavingsGoal (personName: string) (goal: float) =
    appState.Persons <- appState.Persons |> List.map (fun p ->
        if p.Name = personName then { p with SavingsGoal = goal }
        else p)

let addSavings (personName: string) (amount: float) =
    appState.Persons <- appState.Persons |> List.map (fun p ->
        if p.Name = personName then { p with Savings = p.Savings + amount }
        else p)

let exchangeCurrency (personName: string) (newCurrency: string) =
    appState.Persons <- appState.Persons |> List.map (fun p ->
        if p.Name = personName then { p with Currency = newCurrency }
        else p)

// Reminder logic
let updateReminder (personName: string) (frequency: string) (nextDate: DateTime) =
    appState.Persons <- appState.Persons |> List.map (fun p ->
        if p.Name = personName then { p with ReminderFrequency = frequency; NextReminderDate = nextDate }
        else p)

// Functions for calculating savings
let calculateDailySavings (person: Person) = person.Savings / 30.0

let calculateWeeklySavings (person: Person) = person.Savings / 4.0

let calculateMonthlySavings (person: Person) = person.Savings
