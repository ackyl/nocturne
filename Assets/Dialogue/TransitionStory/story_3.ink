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
(Day 2, completed.)
* [>] -> section_b

== section_b ==
~ talking = protagonist_name
(You already feel the weight of working in the field.)
* [>] -> section_c

== section_c ==
~ talking = protagonist_name
~ dialogue_state = "transition"
(A couple of drinks to end the day won’t hurt, right?)
* [>] -> section_1

== section_1 ==
~ dialogue_state = "start"
~ talking = visitor_name
S: Listen, kid. There are still invalid code for today, it's 'Moon'.
* [>] -> section_2
* [Skip] -> section_end

== section_2 ==
~ talking = protagonist_name
{protagonist_name}: Understood.
* [>] -> section_3

== section_3 ==
~ talking = visitor_name
S: From now on, pay attention to any details that seems out of place.
* [>] -> section_3a

== section_3a ==
~ talking = visitor_name
S: Our enemy is closer than we think.
* [>] -> section_4

== section_4 ==
~ talking = protagonist_name
{protagonist_name}: Yes, sir!
* [>] -> section_5

== section_5 ==
~ talking = protagonist_name
(Call ended)
* [>] -> section_end

// DIALOGUE STATE = END
== section_end ==
~ dialogue_state = "end"
-> END
