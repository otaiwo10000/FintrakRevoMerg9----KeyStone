/**
 * Created by Deb on 8/20/2014.
 */
(function () {
    "use strict";
    angular
        .module("fintrak")
        .controller("FTPRiskRatingsListController",
                    ['$scope', '$window', '$state', 'viewModelHelper', 'validator',
                        FTPRiskRatingsListController]);

    function FTPRiskRatingsListController($scope, $window, $state, viewModelHelper, validator) {
        var vm = this;
        vm.viewModelHelper = viewModelHelper;
        vm.parentController = $scope.$parent;

        vm.module = 'MPR Core';
        vm.view = 'ftpriskratings-list-view';
        vm.viewName = 'FTP Risk Ratings';

        vm.viewModelHelper.modelIsValid = true;
        vm.viewModelHelper.modelErrors = [];

        vm.fTPRiskRatingss = [];

        vm.searchvalue = null;

        vm.selectedId = '';
        $scope.selection = [];
        vm.init = false;
        vm.showInstruction = false;
        vm.instruction = '';

        var initialize = function () {

            if (vm.init === false) {
                vm.viewModelHelper.apiGet('api/ftpriskratings/availableftpriskratings', null,
                   function (result) {
                       vm.fTPRiskRatingss = result.data;
                       InitialView();
                       vm.init === true;

                   },
                 function (result) {
                     toastr.error(result.data, 'Fintrak');
                 }, null);
            }
        }

        var InitialView = function () {
            InitialGrid();
        }

        var InitialGrid = function () {
            setTimeout(function () {

                // data export
                if ($('#fTPRiskRatingsTable').length > 0) {
                    var exportTable = $('#fTPRiskRatingsTable').DataTable({
                        "lengthMenu": [[20, 50, 50, 100, -1], [20, 50, 50, 100, "All"]],
                        sDom: "T<'clearfix'>" +
                            "<'row'<'col-sm-6'l><'col-sm-6'f>r>" +
                            "t" +
                            "<'row'<'col-sm-6'i><'col-sm-6'p>>",
                        "tableTools": {
                            "sSwfPath": "app/assets/js/plugins/datatable/exts/swf/copy_csv_xls_pdf.swf"
                        }
                    });
                }
            }, 50);
        }

        initialize();

        vm.icflLoad = function () {

            vm.fTPRiskRatingss = [];
            if (vm.searchvalue == " ") {
                vm.searchvalue = null;
                alert("Search box is empty");
                return
            }

            //vm.viewModelHelper.apiGet('api/custaccount/getcustaccount/' + vm.selectedsearchType + '/' + vm.searchValue + '/' + vm.number, null,
            vm.viewModelHelper.apiGet('api/ftpriskratings/getftpriskratingsusingsearch/' + vm.searchvalue, null,
                function (result) {

                    vm.fTPRiskRatingss = result.data;
                },
                function (result) {
                    toastr.error(result.data, 'Fintrak');
                }, null);
        }

        vm.refreshICFL = function () {
            vm.fTPRiskRatingss = [];
            vm.viewModelHelper.apiGet('api/ftpriskratings/availableftpriskratings', null,
                function (result) {

                    vm.fTPRiskRatingss = result.data;
                },
                function (result) {
                    toastr.error(result.data, 'Fintrak');
                }, null);
        };

    }
}());
