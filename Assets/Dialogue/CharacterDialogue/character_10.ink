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
Guten Abend. Request initiated: one room. No reservation.
* [>] -> section_2
* [Skip] -> section_11

== section_2 ==
~ talking = protagonist_name
Guten Abend, welcome to Nocturne. May I scan your ID to confirm your details?
* [>] -> section_3

== section_3 ==
~ talking = visitor_name
Confirmed. ID ready. Proceed with scan.
* [>] -> section_4

== section_4 ==
~ talking = protagonist_name
Thank you, Mr. Mond. Could you also fill out this document with your preferences and requests for your stay?
* [>] -> section_5

== section_5 ==
~ talking = visitor_name
Acknowledged. Document accepted. Writing… preferences.
* [>] -> section_6

== section_6 ==
~ dialogue_state = "get_document"
Input complete.
* [>] -> section_7

== section_7 ==
~ dialogue_state = "finish_document"
~ talking = protagonist_name
Thank you. I see you’ve requested ramen and tempura.
* [>] -> section_8

== section_8 ==
Great choices, Mr. Mond. May I suggest adding sushi to your meal? It pairs perfectly with tempura.
* [>] -> section_9

== section_9 ==
~ talking = visitor_name
Sushi… recommended. Approved. Add to… order.
* [>] -> section_10

== section_10 ==
One more request. Top room. To… view the moon. Tonight. Necessary.
* [>] -> section_11

== section_11 ==
~ dialogue_state = "card"
~ talking = protagonist_name
Understood, Mr. Mond. Let me check availability.
* [x] -> section_12
* [x] -> section_12

== section_12 ==
~ dialogue_state = "card_given"
You’re in luck. We have a room on the top floor available—room 1203. Here’s your access card.
* [>] -> section_13

== section_13 ==
~ talking = visitor_name
Room… cards... secured. Moonlight… optimal.
* [>] -> section_14

== section_14 ==
Auf Wiedersehen.
* [>] -> section_end

// DIALOGUE STATE = END
== section_end ==
~ dialogue_state = "end"
-> END
