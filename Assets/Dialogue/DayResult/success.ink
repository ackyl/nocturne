// DEFINE VARIABLES - ONLY CHANGE visitor_name and protagonist_name
VAR visitor_name = ""
VAR protagonist_name = ""
VAR dialogue_state = ""
VAR talking = ""
VAR mistake = ""

// DIALOGUE STATE = START
-> visitor_arrival

== visitor_arrival ==
~ talking = visitor_name
Success!

* [>] -> end_conversation

// DIALOGUE STATE = END
== end_conversation ==
~ dialogue_state = "end"
-> END
