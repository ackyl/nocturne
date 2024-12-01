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
(Before wrapping up the day, you receive a call from room 510.)
* [>] -> section_b

== section_b ==
~ talking = protagonist_name
~ dialogue_state = "transition"
(When you arrive, the room is empty except for an unused gun and a note...)
* [>] -> section_1

== section_1 ==
~ dialogue_state = "start"
~ talking = visitor_name
???: You will soon hear the news.
* [>] -> section_2

== section_2 ==
~ dialogue_state = "start"
~ talking = visitor_name
???: Rest assured, everything will be taken care of.
* [>] -> section_3

== section_3 ==
~ dialogue_state = "start"
~ talking = visitor_name
???: Remember, kid. No half measures.
* [>] -> section_end

// DIALOGUE STATE = END
== section_end ==
~ dialogue_state = "end"
-> END
