<div class="sfShortField100">
    <select id="leadTypeSelector"/>
</div>
<div id="decLink" style="display: none" class="sfLicenseExpWidget sfMBottom15">
    <p>
      {$LeadScorePersonalizationResources, NoLeadScoresFound$}
    </p>
</div>
<div style="display:none" id="connectonStatus" class="sfError">
    <label>{$LeadScorePersonalizationResources, UnableToConnect$}</label>
</div>
<div id="loadingSection" style="display: none;" class="sfLoadingDataImage">
</div>
<script type="text/javascript">
    var rootUrl = top.window.location.toString().toLowerCase();
    var sitefinityIndex = rootUrl.indexOf("/sitefinity");
    rootUrl = top.window.location.toString().substring(0, sitefinityIndex);

    var request = {};
    $("#loadingSection").show();
    $("#leadTypeSelector").hide();
    $.ajax({
        type: "get",
        url: rootUrl + '/CustomServices/LeadScoring/GetLeadScoringTypes',
        dataType: 'json',
        data: request,
        success: function (data) {
            var select = document.getElementById("leadTypeSelector");
            var leadTypes = JSON.parse(data).items;
            if (leadTypes.length > 0) {
                $("#loadingSection").hide();
                $("#leadTypeSelector").show();
                for (var i = 0; i < leadTypes.length; i++) {
                    var leadTypeId = leadTypes[i].Id
                    var leadTypeName = leadTypes[i].Name;
                    var leadTypeLevels = leadTypes[i].Levels;
                    for (var j = 0; j < leadTypeLevels.length; j++) {
                        var option = document.createElement("option");
                        option.text = leadTypeName + " -- " + leadTypeLevels[j].Name;
                        option.value = leadTypeId + "--" + leadTypeLevels[j].Id;
                        select.add(option);
                    }

                }
            }
            else
            {
                $("#loadingSection").hide();
                var decLink = $("#decLink")[0];
                select.style.display = "none";
                decLink.style.display = "block";
            }
        },
        error: function (data) {
            $("#loadingSection").hide();
            var select = document.getElementById("leadTypeSelector");
            var connectionStatus = $("#connectonStatus")[0];
            select.style.display = "none";
            connectionStatus.style.display = "block";
        }
    });

    function CriterionEditor() {
    }

    CriterionEditor.prototype = {

        //Used as a label for the criterion when viewing the user segment
        getCriterionTitle: function () {
            return "{$LeadScorePersonalizationResources, LeadScoreCriterionTitle$}";
        },

        //The descriptive value for the criterion
        getCriterionDisplayValue: function () {
            return $("#leadTypeSelector option:selected").text();
        },

        //Persists the input from the user as a value for the criterion
        getCriterionValue: function () {
            return $("#leadTypeSelector").val();
        },

        //When the editor opens, sets the previously persisted value as initial value for the criterion
        setCriterionValue: function (val) {
            $("#leadTypeSelector").val(val);
        },

        errorMessage: "",

        isValid: function () {
            return true;
        }
    };
</script>