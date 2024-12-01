// DEFINE VARIABLES - ONLY CHANGE visitor_name and protagonist_name
VAR visitor_name = ""
VAR protagonist_name = ""
VAR dialogue_state = ""
VAR talking = ""
VAR mistake = ""

// DIALOGUE STATE = START
-> section_a

== section_a ==
~ talking = protagonist_name
(Later that night, in a sudden. You feel a sharp pain in your head.)
* [>] -> section_b

== section_b ==
~ talking = protagonist_name
(Something is wrong... Before you can reach for a call,)
* [>] -> section_c

== section_c ==
~ talking = protagonist_name
~ dialogue_state = "transition"
(You hear a faint laugh from afar and pass out.)
* [>] -> section_1

== section_1 ==
~ dialogue_state = "start"
~ talking = visitor_name
Kael: Burrnnn ... gerr ...
* [>] -> section_2

== section_2 ==
~ dialogue_state = "switch"
Catarina: I think it's the same heat as a sunlight.
* [>] -> section_2a

== section_2a ==
~ dialogue_state = "switch"
Catarina: Beautiful, blinding.
* [>] -> section_3

== section_3 ==
~ dialogue_state = "switch"
Thome: Did my donut get overcooked?
* [>] -> section_3a

== section_3a ==
~ dialogue_state = "switch"
Thome: heHahAHaHAHaHA!
* [>] -> section_4

== section_4 ==
~ dialogue_state = "switch"
Park: Finally, the perfect way to roast.
* [>] -> section_end

// DIALOGUE STATE = END
== section_end ==
~ dialogue_state = "end"
-> END
