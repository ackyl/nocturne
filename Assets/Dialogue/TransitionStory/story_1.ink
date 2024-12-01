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
("To be assigned for Nocturne—)
* [>] -> section_b

== section_b ==
~ talking = protagonist_name
(is to keep your head low and your eyes sharp.")
* [>] -> section_c

== section_c ==
~ talking = protagonist_name
~ dialogue_state = "transition"
(That's what they always say back at HQ.)
* [>] -> section_1

== section_1 ==
~ talking = visitor_name
~ dialogue_state = "scene"
S: Listen up, kid. You’re here to proof nothing. Your job is simple—
* [>] -> section_2
* [Skip] -> section_end

== section_2 ==
S: Distinguish civilians, enemy agents, and our own. If you fail, Nocturne’s cover is blown.
* [>] -> section_3

== section_3 ==
~ talking = protagonist_name
{protagonist_name}: Understood. Mr. Spectre.
* [>] -> section_4

== section_4 ==
~ talking = visitor_name
S: !!!
* [>] -> section_4a

== section_4a ==
S: Never mention names in this communication.
* [>] -> section_5

== section_5 ==
~ talking = protagonist_name
{protagonist_name}: Sorry, sir. Would never happen again.
* [>] -> section_6

== section_6 ==
~ talking = visitor_name
S: —and don't mess this simple task, or C3 will kill you.
* [>] -> section_7

== section_7 ==
~ talking = protagonist_name
{protagonist_name}: My dad would kill me?
* [>] -> section_8

== section_8 ==
~ talking = visitor_name
S: Yeah, Champagne, along with Cognac and Clairette, never hesitates.
* [>] -> section_9

== section_9 ==
S: —even for me, or their own family.
* [>] -> section_10

== section_10 ==
~ talking = protagonist_name
{protagonist_name}: ...I won’t fail then.
* [>] -> section_11

== section_11 ==
~ talking = visitor_name
S: Good. You better not.
* [>] -> section_12

== section_12 ==
~ talking = visitor_name
S: Now, pay attention. You’ll receive daily encrypted messages from me through the ICX.
* [>] -> section_13

== section_13 ==
S: Inside, you’ll find the daily code to identify allies.
* [>] -> section_14

== section_14 ==
~ talking = protagonist_name
{protagonist_name}: (The ICX. Finally got my hand on this special gadget)
* [>] -> section_15

== section_15 ==
{protagonist_name}: Understood. I’ll be ready tomorrow night.
* [>] -> section_16

== section_16 ==
~ talking = visitor_name
S: You’d better be.
* [>] -> section_17

== section_17 ==
S: Remember, kid: you’re expendable. Nocturne isn’t.
* [>] -> section_18

== section_18 ==
~ talking = protagonist_name
(Call ended.)
* [>] -> section_end

// DIALOGUE STATE = END
== section_end ==
~ dialogue_state = "end"
-> END
