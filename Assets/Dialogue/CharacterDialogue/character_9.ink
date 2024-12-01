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
Konnichiwa! I need… room, please.
* [>] -> section_2
* [Skip] -> section_8

== section_2 ==
~ talking = protagonist_name
Konnichiwa, welcome to Nocturne. May I scan your ID to confirm your details?
* [>] -> section_3

== section_3 ==
~ talking = visitor_name
Yes. Please. ID.
* [>] -> section_4

== section_4 ==
~ talking = protagonist_name
Thank you, Mr. Hanzo. Could you also fill out this document with your preferences and requests for your stay?
* [>] -> section_5

== section_5 ==
~ talking = visitor_name
Ah… okay. Write… here?
* [>] -> section_6

== section_6 ==
~ talking = protagonist_name
Yes, just list your preferences and any special requests.
* [>] -> section_7

== section_7 ==
~ dialogue_state = "get_document"
~ talking = visitor_name
Okay, document, here.
* [>] -> section_7a

== section_7a ==
~ dialogue_state = "finish_document"
~ talking = protagonist_name
Thank you. I see you’ve requested a twin bed.
* [>] -> section_8

== section_8 ==
~ dialogue_state = "card"
~ talking = protagonist_name
Let me confirm that for you.
* [x] -> section_9
* [x] -> section_9a

== section_9 ==
~ dialogue_state = "card_given"
Your twin bed is all set. Your room is ready—room 205 on the second floor. Here’s your access card.
* [>] -> section_10

== section_9a ==
~ dialogue_state = "card_given"
Your twin bed is all set. Your room is ready—room 205 on the second floor. Here’s your access card.
* [>] -> section_12

== section_10 ==
~ talking = visitor_name
Twin bed… very nice. Thank you!
* [>] -> section_11

== section_11 ==
Nocturne… very good place.
* [>] -> section_13

== section_12 ==
~ talking = visitor_name
You know what, I actually need more than this.
* [>] -> section_14

== section_13 ==
~ talking = protagonist_name
I’m glad to hear that. Enjoy your stay, Mr. Hanzo.
* [>] -> section_end

== section_14 ==
~ talking = protagonist_name
If you need anything further, don’t hesitate to let us know.
* [>] -> section_end

// DIALOGUE STATE = END
== section_end ==
~ dialogue_state = "end"
-> END
