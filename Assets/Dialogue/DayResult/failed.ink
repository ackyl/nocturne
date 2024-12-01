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
(You finish your day as usual, just before the station closes.)
* [>] -> section_b

== section_b ==
~ talking = protagonist_name
~ dialogue_state = "transition"
(But just as you are about to reach the station...)
* [>] -> section_1

== section_1 ==
~ talking = visitor_name
???: What a disappointment.
* [>] -> section_2
* [Skip] -> section_end

== section_2 ==
~ talking = protagonist_name
{protagonist_name}: Sorry, do I know you?
* [>] -> section_3

== section_3 ==
~ talking = visitor_name
???: You failed, {mistake} people got the wrong room.
* [>] -> section_4

== section_4 ==
???: You exposed Nocturne.
* [>] -> section_5

== section_5 ==
~ talking = protagonist_name
{protagonist_name}: ...?
* [>] -> section_6

== section_6 ==
~ talking = visitor_name
???: Just as I thought you're really not worthy of your father.
* [>] -> section_7

== section_7 ==
~ dialogue_state = "gun"
~ talking = protagonist_name
!!!
* [>] -> section_end

// DIALOGUE STATE = END
== section_end ==
~ dialogue_state = "end"
-> END
