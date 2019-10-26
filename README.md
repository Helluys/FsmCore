# FsmCore
Editable Finite State Machines for Unity

# How to use
Clone the repository in your Unity project's Package directory

This is still in development and has very limited features.

Currently available features are accessible via C# API:
* Define your own state behavious by inheriting FsmState and implementing OnEnter, OnStay and OnExit
* Define your parameters and constants among Trigger, Boolean, Integer and Float
* Define your condition by adding parameters and constants to them
* Define your transitions by defining the conditions and the target state
* FiniteStateMachine that hold eveything together and is updated via its Update method

Serialization is almost complete, but needs improvements to prevent creating invalid state machines.

Upcoming feature is a UI editor based on UIElements.
