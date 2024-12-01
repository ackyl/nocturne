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
(Day 1, completed.)
* [>] -> section_b

== section_b ==
~ talking = protagonist_name
~ dialogue_state = "transition"
(You feel a slight sense of relief. Confident that all is well for now.)
* [>] -> section_1

== section_1 ==
~ dialogue_state = "start"
~ talking = visitor_name
S: Hey, kid. One of the codes you receive today will be invalid.
* [>] -> section_2
* [Skip] -> section_end

== section_2 ==
~ talking = protagonist_name
{protagonist_name}: Invalid? May I know why, sir?
* [>] -> section_3

== section_3 ==
~ talking = visitor_name
S: No questions.
* [>] -> section_4

== section_4 ==
S: Someone using the 'France' code today could be an enemy,
* [>] -> section_5

== section_5 ==
S: —or it could be worse.
* [>] -> section_6

== section_6 ==
~ talking = protagonist_name
{protagonist_name}: Understood.
* [>] -> section_7

== section_7 ==
~ talking = protagonist_name
(Call ended)
* [>] -> section_end

// DIALOGUE STATE = END
== section_end ==
~ dialogue_state = "end"
-> END
