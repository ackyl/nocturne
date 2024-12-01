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
(Although you finished today's work, you feel unusually tired today.)
* [>] -> section_b

== section_b ==
~ talking = protagonist_name
~ dialogue_state = "transition"
(Maybe it's because you relied more on your instincts today than ever before.)
* [>] -> section_1

== section_1 ==
~ dialogue_state = "start"
~ talking = visitor_name
S: Hey kid, can you hear me?
* [>] -> section_1a
* [Skip] -> section_end

== section_1a ==
~ talking = protagonist_name
{protagonist_name}: Yes, sir.
* [>] -> section_1b

== section_1b ==
~ talking = visitor_name
S: Good. I didn’t think I’d say this, kid. But you did well yesterday.
* [>] -> section_2

== section_2 ==
~ talking = visitor_name
S: Our ICX channel was hacked yesterday, all code is leaked.
* [>] -> section_3

== section_3 ==
~ talking = protagonist_name
{protagonist_name}: (Thank god I was right...)
* [>] -> section_4

== section_4 ==
S: But you figured it out on your own.
* [>] -> section_5

== section_5 ==
~ talking = protagonist_name
{protagonist_name}: Well, I'm just trying my best to pay attention to the details, sir.
* [>] -> section_6

== section_6 ==
~ talking = visitor_name
S: Well done. Now, get back to work.
* [>] -> section_7

== section_7 ==
~ talking = protagonist_name
(Call ended)
* [>] -> section_end

// DIALOGUE STATE = END
== section_end ==
~ dialogue_state = "end"
-> END
