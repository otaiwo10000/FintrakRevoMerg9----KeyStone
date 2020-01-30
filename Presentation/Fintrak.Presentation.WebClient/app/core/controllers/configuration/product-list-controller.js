/**
 * Created by Deb on 8/20/2014.
 */
(function () {
    "use strict";
    angular
        .module("fintrak")
        .controller("ProductListController",
                    ['$rootScope', '$scope', '$state', 'viewModelHelper', 'validator', 'UploadService',
                        ProductListController]);

    function ProductListController($rootScope,$scope, $state, viewModelHelper, validator, UploadService) {
        var vm = this;
        vm.viewModelHelper = viewModelHelper;
        vm.parentController = $scope.$parent;

        vm.module = 'Core';
        vm.view = 'product-list-view';
        vm.viewName = 'Products';
        var exportTable= {};
        vm.viewModelHelper.modelIsValid = true;
        vm.viewModelHelper.modelErrors = [];
        
        vm.products = [];

        vm.init = false;
        vm.showInstruction = false;
        vm.instruction = '';

        vm.csv = {
            uploadCode: 'COR001',
            content: null,
            header: true,
            headerVisible: false,
            separator: ',',
            separatorVisible: false,
            result: null,
            encoding: 'ISO-8859-1',
            encodingVisible: true,
        };

        vm.importData = function () {
            UploadService.runUpload(vm.csv);
        }

        var initialize = function(){

            if (vm.init === false) {
                $rootScope.$on('uploadCompleted', onUploadCompleted);
                vm.viewModelHelper.apiGet('api/product/availableproducts', null,
                   function (result) {
                       vm.products = result.data;
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
                if ($('#productTable').length > 0) {
                     exportTable = $('#productTable').DataTable({
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

        vm.load = function () {
            var url = '';
            url = 'api/product/getproductbycode/' + vm.products.Code,

            vm.viewModelHelper.apiGet(url, null,
               function (result) {
                   vm.products = result.data;

                   exportTable = exportTable.destroy();
                   InitialView();
                   toastr.success('Data for the specified Product Code loaded.', 'Fintrak');
               },
               function (result) {
                   toastr.error('Fail to load data for the Product Code.', 'Fintrak');
               }, null);
        }


        vm.reloadPage = function () {

            vm.viewModelHelper.apiGet('api/product/availableproducts', null,
                   function (result) {
                       vm.products = result.data;
                       exportTable = exportTable.destroy();
                       InitialView();
                       vm.init === true;

                   },
                 function (result) {
                     toastr.error(result.data, 'Fintrak');
                 }, null);
        }

        var onUploadCompleted = function () {
            vm.viewModelHelper.apiGet('api/product/availableproducts', null,
                  function (result) {
                      vm.products = result.data;
                      //
                  },
                 function (result) {
                     toastr.error(result.data, 'Fintrak');
                 }, null);
        }

        initialize(); 
    }
}());
