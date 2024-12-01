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
(Day 3, done.)
* [>] -> section_b

== section_b ==
~ talking = protagonist_name
(You start to wonder about the invalid code in the ICX. And the decision you made that day.)
* [>] -> section_c

== section_c ==
~ talking = protagonist_name
(You definitely need a good rest tonight.)
* [>] -> section_end

// DIALOGUE STATE = END
== section_end ==
~ dialogue_state = "end"
-> END
