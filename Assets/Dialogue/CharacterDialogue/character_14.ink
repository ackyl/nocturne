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
Good Evening. I believe you already have my reservation—Catarina Bleu.
* [>] -> section_2
* [Skip] -> section_10

== section_2 ==
~ talking = protagonist_name
Good Evening, Ms. Bleu. Welcome to Nocturne.
* [>] -> section_3

== section_3 ==
May I scan your ID to confirm your booking?
* [>] -> section_4

== section_4 ==
~ talking = visitor_name
Of course. I expect everything is in order.
* [>] -> section_5

== section_5 ==
~ dialogue_state = "get_document"
~ talking = protagonist_name
Thank you, Ms. Bleu. I see your reservation here.
* [>] -> section_6

== section_6 ==
~ dialogue_state = "finish_document"
You’ve requested a brand new bed sheet.
* [>] -> section_7

== section_7 ==
—and a premium room with plenty of sunlight.
* [>] -> section_8

== section_8 ==
~ talking = visitor_name
Sunlight is… essential for my energy.
* [>] -> section_9

== section_9 ==
A proper start to the day requires the perfect setting, don’t you agree?
* [>] -> section_9a

== section_9a ==
Oh, I nearly forgot. Would you like to help me to park my car?
* [>] -> section_9b

== section_9b ==
~ talking = protagonist_name
Absolutely, I'll ask our officers to park your car.
* [>] -> section_10

== section_10 ==
~ dialogue_state = "card"
~ talking = protagonist_name
—and please let me confirm the details for you.
* [x] -> section_11
* [x] -> section_11

== section_11 ==
~ dialogue_state = "card_given"
You’re all set, Ms. Bleu. Your room—808 on the eighth floor—
* [>] -> section_12

== section_12 ==
it offers a beautiful sunrise view and plenty of sunlight.
* [>] -> section_13

== section_13 ==
~ talking = visitor_name
Exquisite. I trust the arrangements will be as flawless as promised.
* [>] -> section_14

== section_14 ==
~ talking = protagonist_name
Thank you, Ms Bleu.
* [>] -> section_15

== section_15 ==
If there’s anything else you need during your stay, please don’t hesitate to let us know.
* [>] -> section_16

== section_16 ==
~ talking = visitor_name
I shall. Have a pleasant evening.
* [>] -> section_end

// DIALOGUE STATE = END
== section_end ==
~ dialogue_state = "end"
-> END
