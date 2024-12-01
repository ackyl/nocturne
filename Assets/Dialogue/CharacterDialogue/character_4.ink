// DEFINE VARIABLES - ONLY CHANGE visitor_name and protagonist_name
VAR visitor_name = ""
VAR protagonist_name = ""
VAR dialogue_state = ""
VAR talking = ""

// DIALOGUE STATE = START
-> section_1

== section_1 ==
~ dialogue_state = "start"
~ talking = visitor_name
...
* [>] -> section_2
* [Skip] -> section_10

== section_2 ==
~ talking = protagonist_name
Good evening, welcome to Nocturne. May I scan your ID to confirm your booking?
* [>] -> section_3

== section_3 ==
~ talking = visitor_name
Fine. Just don’t waste my time.
* [>] -> section_4

== section_4 ==
~ talking = protagonist_name
Thank you, Ms. Elysium. Are you visiting for business or leisure?
* [>] -> section_5

== section_5 ==
~ talking = visitor_name
Leisure, if you must know. And I expect perfection. I’ve endured enough mediocrity.
* [>] -> section_6

== section_6 ==
~ talking = protagonist_name
Understood. Could you please fill out this document with your details and preferences for your stay?
* [>] -> section_7

== section_7 ==
~ dialogue_state = "get_document"
~ talking = visitor_name
Hmph. At least you’re efficient. Here, just take a look.
* [>] -> section_8

== section_8 ==
~ talking = protagonist_name
~ dialogue_state = "finish_document"
Thank you. I see you’ve requested a room on the eleventh floor for the view. Let me check that for you.
* [>] -> section_9

== section_9 ==
~ talking = visitor_name
I didn’t request it. I demanded it. The view had better be worth my time.
* [>] -> section_10

== section_10 ==
~ dialogue_state = "card"
~ talking = protagonist_name
Of course, Ms. Elysium. Wait a moment please.
* [x] -> section_11
* [x] -> section_11

== section_11 ==
~ dialogue_state = "card_given"
I’m pleased to confirm your room is ready—room 1101. I trust it will meet your expectations.
* [>] -> section_12

== section_12 ==
~ talking = visitor_name
It had better. I have no tolerance for disappointment.
* [>] -> section_13

== section_13 ==
~ talking = protagonist_name
Enjoy your stay at Nocturne, Ms. Elysium. Should you need anything further, don’t hesitate to let us know.
* [>] -> section_14

== section_14 ==
~ talking = visitor_name
I’ll be the judge of what I need.
* [>] -> section_end

// DIALOGUE STATE = END
== section_end ==
~ dialogue_state = "end"
-> END
