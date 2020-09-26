$(document).ready(function () {
    // Initialize the Chat
    var QUOTE_CARD_TEMPLATE = kendo.template($('#quote-template').html());
    kendo.chat.registerTemplate("quote", QUOTE_CARD_TEMPLATE);
    var chat = $("#chat").kendoChat({
        post: function (args) {
            agent.postMessage(args);
        }
    }).data("kendoChat");

    // Create a new agent and pass the Chat widget.
    var agent = new ChatAgent(chat);
    var QUOTE_CARD_TEMPLATE = kendo.template(
        '<div class="k-card k-card-type-rich">' +
        '<p><strong>Project Details :</strong></p>' +
        '<div> <img width=250 height=250 style="object-fit: cover;" src="#:projectImageURL#" />  </div>' +
        '<div class="k-card-body quoteCard">' +
        '<div><strong>Project Name:</strong>' +
        '<span>#:project#</span></div>' +

        '<div><strong>Location :</strong>' +
        '<span>#:location#</span></div>' +

        '<div><strong>ClubHouse:</strong>' +
        '<span>#:chouse#</span></div>' +

        '</div>' +
        '</div>'
    );
    kendo.chat.registerTemplate("quote", QUOTE_CARD_TEMPLATE);

    var QUOTE_CARD_TEMPLATE1 = kendo.template(
        '<div class="k-card k-card-type-rich">' +
        '<img width=250 height=250 style="object-fit: cover;" src="#:projectImageURL#" alt = "undefined" class= "k-card-image" >' +
        '<div class="k-card-body"><h5 class="k-card-title">#:project#</h5>' +
        '<h6 class="k-card-subtitle">#:location#</h6>' +
        '<p style = "line-height: 1.0;font-size: small;">#:description#</p >'
        + '</div>'
        + '<div class= "k-card-actions k-card-actions-vertical" >' +
        '<span class="k-card-action">' +
        '<a  href="#:locationUrl#" class= "k-button k-flat k-primary" > Get Location Map</span ></a >' +
        '</div></div>'
    );
    kendo.chat.registerTemplate("quote1", QUOTE_CARD_TEMPLATE1);


    var PLAN_CARD_TEMPLATE = kendo.template(

        '<div class="k-quick-replies">' +
        '# for (var i = 0; i < rows.length; i++) { #' +

        '<span class="k-quick-reply" data-value="#:rows[i].value#">#:rows[i].title#</span>' +
        '# } #' +
        '</div>'
    );
    kendo.chat.registerTemplate("payment_plan", PLAN_CARD_TEMPLATE);



    var AMENITIES_TEMPLATE = kendo.template(
        //'<div class="k-card-deck">'+

        '# for (var i = 0; i < rows.length; i++) { #' +
        '<div class="k-card k-card-type-rich">' +
        '<img width=250 height=250 style="object-fit: cover;" src="#: rows[i].AmenitesPicUrl#" alt = "undefined" class= "k-card-image" >' +
        '<div class="k-card-body">' +
        '<h5 class= "k-card-title" >#: rows[i].Amenities#</h5 >' +
        '</div>' +
        '</div>' +
        '# } #'
        //'</div>'

    );
    kendo.chat.registerTemplate("Amenities", AMENITIES_TEMPLATE);
    // Initially render a hint message to the user.
    agent.chat.renderMessage({
        type: "text",
        text: "Hello! Please choose your preferred location.",
        timestamp: new Date()
    }, {
        name: "Bot",
        iconUrl: "https://demos.telerik.com/kendo-ui/content/chat/InsuranceBot.png"
    });
    $.ajax({
        type: "GET",
        contentType: "application/json; charset=utf-8",
        url: "/ChatBot/GetProjectLocationsForChatBot",
        success: function (data) {
            //chat.renderSuggestedActions(data);
            chat.renderAttachments({
                attachments: [{
                    contentType: "payment_plan",
                    content: {
                        rows: data
                    }
                }]
                //,attachmentLayout: "carousel"
            }, "Venkat");
        },
        error: function (data) {

        }
    });

});


window.ChatAgent = kendo.Class.extend({
    // Initialize the ChatAgent object.
    init: function (chat) {
        this.chat = chat;

    },
    // Implement the postMessage logic.
    postMessage: function (args) {
        var chat = this.chat;

        if (args.text.includes("Project-")) {
            var projectName = args.text.split("-").pop();
            $.ajax({
                type: "GET",
                contentType: "application/json; charset=utf-8",
                url: "/ChatBot/GetProjectSuggestionForChatBot",
                data: { projectName: projectName },
                success: function (data) {
                    chat.renderMessage({
                        type: "text",
                        text: "Select the below options to know more about :" + projectName,
                        timestamp: new Date()
                    }, {
                        name: "Bot",
                        iconUrl: "https://demos.telerik.com/kendo-ui/content/chat/InsuranceBot.png"
                    });
                    chat.renderSuggestedActions(data);


                },
                error: function (data) {
                }
            });


        }
        else if (args.text.includes("Location-")) {
            var locationName = args.text.split("-").pop();
            $.ajax({
                type: "GET",
                contentType: "application/json; charset=utf-8",
                url: "/ChatBot/GetProjectBasedOnLocationsForChatBot",
                data: { locationName: locationName },
                success: function (data) {
                    chat.renderMessage({
                        type: "text",
                        text: "Please choose your preferred Project.",
                        timestamp: new Date()
                    }, {
                        name: "Bot",
                        iconUrl: "https://demos.telerik.com/kendo-ui/content/chat/InsuranceBot.png"
                    });
                    chat.renderSuggestedActions(data);


                },
                error: function (data) {
                }
            });
        }
        else if (args.text.includes("-Details")) {
            var projectName = args.text.split("-");
            $.ajax({
                type: "GET",
                contentType: "application/json; charset=utf-8",
                url: "/ChatBot/GetProjectDetailsForChatBot",
                data: { projectName: projectName[0] },
                success: function (data) {
                    chat.renderMessage({
                        type: "text",
                        text: "Here are the project details for " + projectName[0],
                        timestamp: new Date()
                    }, {
                        name: "Bot",
                        iconUrl: "https://demos.telerik.com/kendo-ui/content/chat/InsuranceBot.png"
                    });
                    chat.renderAttachments({
                        attachments: [{
                            contentType: "quote1",
                            content: {
                                "project": data.ProjectName,
                                "location": data.ProjectLocation,
                                "locationUrl": data.LocationURL,
                                "chouse": data.ClubHouseCharges,
                                "description": data.Description,
                                "projectImageURL": data.projectImageURL,
                            }
                        }]
                        //,attachmentLayout: "carousel"
                    }, "Venkat");


                    var json = [{
                        title: "Approvals",
                        value: projectName[0] + "-Approvals"
                    },
                    {
                        title: "Amenities",
                        value: projectName[0] + "-Amenities"
                    }];

                    chat.renderSuggestedActions(json);
                },
                error: function (data) {
                }
            });
        }
        else if (args.text.includes("-Approvals")) {
            var projectName = args.text.split("-");
            var approvalText = "";
            if (projectName[0] == 'Jewels County') {
                approvalText = 'HMDA Approved';
            }
            else if (projectName[0] == 'In the Woods') {
                approvalText = 'HMDA Approval to be completed by March 2021';
            }
            else if (projectName[0] == 'Elderado') {
                approvalText = 'HMDA Approved';
            }
            else if (projectName[0] == 'PVR Anmol') {
                approvalText = 'HMDA awaiting for DC Letter';
            }
            else if (projectName[0] == 'Alpine Square') {
                approvalText = 'GHMC & RERA Approved';
            }
            else if (projectName[0] == 'Alpine Vistas') {
                approvalText = 'HMDA Approval Pending';
            }
            $.ajax({
                type: "GET",
                contentType: "application/json; charset=utf-8",
                url: "/ChatBot/GetProjectDetailsForChatBot",
                data: { projectName: projectName[0] },
                success: function (data) {
                    chat.renderMessage({
                        type: "text",
                        text: approvalText,
                        timestamp: new Date()
                    }, {
                        name: "Bot",
                        iconUrl: "https://demos.telerik.com/kendo-ui/content/chat/InsuranceBot.png"
                    });
                    var json = [{
                        title: "Amenities",
                        value: projectName[0] + "-Amenities"
                    },
                    {
                        title: "Project Details",
                        value: projectName[0] + "-Details"
                    }];

                    chat.renderSuggestedActions(json);
                },
                error: function (data) {
                }
            });
        }
        else if (args.text.includes("-Amenities")) {
            var projectName = args.text.split("-");

            $.ajax({
                type: "GET",
                contentType: "application/json; charset=utf-8",
                url: "/ChatBot/GetProjectAmenitiesForChatBot",
                data: { projectName: projectName[0] },
                success: function (data) {
                    //chat.renderSuggestedActions(data);
                    chat.renderMessage({
                        type: "text",
                        text: "Check out the Amenities for " + projectName[0],
                        timestamp: new Date()
                    }, {
                        name: "Bot",
                        iconUrl: "https://demos.telerik.com/kendo-ui/content/chat/InsuranceBot.png"
                    });
                    chat.renderAttachments({
                        attachments: [{
                            contentType: "Amenities",
                            content: {
                                rows: data
                            }
                        }]
                        , attachmentLayout: "carousel"
                    }, "Venkat");

                    var json = [{
                        title: "Approvals",
                        value: projectName[0] + "-Approvals"
                    },
                    {
                        title: "Project Details",
                        value: projectName[0] + "-Details"
                    }];

                    chat.renderSuggestedActions(json);

                },
                error: function (data) {

                }
            });
        }


    }
});