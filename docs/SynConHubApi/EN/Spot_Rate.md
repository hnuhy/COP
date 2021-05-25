# COSCO Syncon Hub API Specification for Spot Rate

[toc]

## Update Logs

v1.2.0 (2021-04-28)

1. **`A `**  Added the API Intermodal Service Query to search product supported transfer services.  [Details](#Intermodal Service Query Interface).
2. **`U`** Updated the spot rate detail query support search product detail with intermodal services. [Details](#Spot Rate Details Query Interface).
3. **`U`** Updated the surcharge query support search product surcharge with intermodal services. [Details](#Surcharge Query Interface).




## Http Header

HTTP header information description

| HTTP Header             | Type   | Required | Description             | Remarks |
| ----------------------- | ------ | -------- | ----------------------- | ------- |
| X-Consumer-Forwarder-ID | String | no       | Forwarder company sapId |         |

- X-Consumer-Forwarder-ID

  ```
  # If you want to submit an order/booking for your subsidiary,add this header to the request.The value is sapid of the subsidiary
  
  X-Consumer-Forwarder-ID: 1234567890
  ```



## City Information Query Interface

Fuzzy query of the city information maintained by the platform based on the keywords of the city name. The returned city id can be used as the origin / destination location id for the Spot Rate query interface.

### 1. Informations

* **Path**: /service/synconhub/common/city/search
* **Method**: POST

### 2. Request parameters

| Name     | **Type** | **Required** |    Description     |                  **Remarks**                  |
| :------- | :------: | :----------: | :----------------: | :-------------------------------------------: |
| keywords |  String  |     yes      | city name keywords |                                               |
| page     | Integer  |      no      | page number        |       **default:**1，**minimum:**1          |
| size     | Integer  |      no      | page size          | **default:**30，**minimum:**1，**maximum:**30 |

### 3. Request sample

```javascript
{
    "keywords": "shenzhen",
    "page": 1,
    "size": 30
}
```

### 4. Response parameters

| Name    | Description                                |
| ------- | ------------------------------------------ |
| code    | status code                                |
| message | detail message                             |
| data    | Paging query results (sorted by city name) |

### 5. Response sample

```javascript
{
    "code": 0,
    "message": "",
    "data": {
        "content": [
            {
                "id": "738872886233057", // city id
                "unlocode": "CNSZN", // port code
                "cityName": "Shenzhen",
                "cntyName": "Shenzhen", // county name
                "stateName": "Guangdong",
                "stateCode": "GD",
                "ctryRegionName": "China", // country / region name
                "ctryRegionCode": "CN", // country / region code
                "cityFullNameEn": "Shenzhen",
                "cityFullNameCn": "深圳"
            }
        ],
        "number": 1,
        "size": 30,
        "totalPages": 1,
        "totalElements": 1,
        "first": true, // is first page
        "last": true, // is last page
        "empty": false
    }
}
```

### 6. Error sample

```javascript
{
    "code": 20041,
    "message": "the page size should be between 0 and 30."
}
```



## Spot Rate Query Interface

### 1. Informations

* **Path**: /service/synconhub/product/instantBooking/search
* **Method**: POST

### 2. Request parameters

| Name      |  Type   | Required |             Description              |            **Remarks**            |
| :-------- | :-----: | :------: | :----------------------------------: | :-------------------------------: |
| startDate | ISODate |   yes    | the start date of sailing date scope |                                   |
| endDate   | ISODate |    no    |    the end of Sailing date scope     |                                   |
| porCityId | string  |   yes    |             por city id              |                                   |
| fndCityId | string  |   yes    |             fnd city id              |                                   |
| page      | integer |   yes    |              page size               |   **default:**1，**minimum:**1    |
| size      | integer |   yes    |             record size              | **default:**20，**range:**[1, 50] |

### 3. Request sample

```javascript
{
  "startDate": "2020-06-01T00:00:00.000Z",
  "endDate": "2020-07-30T00:00:00.000Z",
  "fndCityId": "738872886233842",
  "porCityId": "738872886232873",
  "page": 1,
  "size": 20
}
```

### 4. Response parameters

| Name    | Description                                                  |
| ------- | ------------------------------------------------------------ |
| code    | status code                                                  |
| message | detail message                                               |
| data    | pagination to return product information that meets the query conditions. (sorted by ETD by default) |

### 5. Response sample

```javascript
{
    "code": 0,
    "message": "",
    "data": {
        "content": [
            {
                "id": "8a80cb817321ad030173221c880101d2",
                "tradeLaneCode": "AEU",
                "serviceCode": "AEM5",
                "tradeArea": "ETD",
                "vesselName": "COSCO YANTIAN",
                "voyageNo": "020",
                "direction": "W",
                "porFacilityCode": null,
                "porFacilityName": null,
                "porFacilityNameCn": null,
                "fndFacilityName": null,
                "fndFacilityNameCn": null,
                "fndFacilityCode": null,
                "haulageType": "CY-CY",
                "estimatedTransitTimeInDays": 30,
                "effectiveStartDate": "2020-07-05T00:00:00.000Z", 
                "effectiveEndDate": "2020-12-23T23:59:59.000Z",
                "eta": "2020-09-07 00:00",
                "etd": "2020-08-04 00:00",
                "inventory": 717,
                "polTsMode": null, // pol transit mode
                "podTsMode": null, // pod transit mode
                "porCity": { // por city info，for the specific structure, please refer to 'City information query interface'
                    "id": "738872886233044",
                    "unlocode": "CNSHE",
                    "cityName": "Shekou",
                    "cntyName": "Shenzhen",
                    "stateName": "Guangdong",
                    "stateCode": "GD",
                    "ctryRegionName": "China",
                    "ctryRegionCode": "CN",
                    "cityFullNameEn": "Shekou",
                    "cityFullNameCn": "蛇口"
                },
                "fndCity": {
                    "id": "738872886271836",
                    "unlocode": "ZADUR",
                    "cityName": "Durban",
                    "cntyName": null,
                    "stateName": "KwaZulu-Natal",
                    "stateCode": null,
                    "ctryRegionName": "South Africa",
                    "ctryRegionCode": "ZA",
                    "cityFullNameEn": "Durban",
                    "cityFullNameCn": "德班"
                },
                "polPort": {
                    "id": "349645770723389",
                    "portName": "Shekou", 
                    "portCode": "SHK",
                    "portFullNameEn": "Shekou",
                    "portFullNameCn": "蛇口",
                    "city": {
                        "id": "738872886233044",
                        "unlocode": "CNSHE",
                        "cityName": "Shekou",
                        "cntyName": "Shenzhen",
                        "stateName": "Guangdong",
                        "stateCode": "GD",
                        "ctryRegionName": "China",
                        "ctryRegionCode": "CN",
                        "cityFullNameEn": "Shekou",
                        "cityFullNameCn": "蛇口"
                    }
                },
                "podPort": {
                    "id": "349647918206992",
                    "portName": "Durban",
                    "portCode": "DUR",
                    "portFullNameEn": "Durban",
                    "portFullNameCn": "Durban",
                    "city": {
                        "id": "738872886271836",
                        "unlocode": "ZADUR",
                        "cityName": "Durban",
                        "cntyName": null,
                        "stateName": "KwaZulu-Natal",
                        "stateCode": null,
                        "ctryRegionName": "South Africa",
                        "ctryRegionCode": "ZA",
                        "cityFullNameEn": "Durban",
                        "cityFullNameCn": "德班"
                    }
                },
                "firstPolPort": {
                    "id": "349645770723389",
                    "portName": "Shekou",
                    "portCode": "SHK",
                    "portFullNameEn": "Shekou",
                    "portFullNameCn": "蛇口",
                    "city": {
                        "id": "738872886233044",
                        "unlocode": "CNSHE",
                        "cityName": "Shekou",
                        "cntyName": "Shenzhen",
                        "stateName": "Guangdong",
                        "stateCode": "GD",
                        "ctryRegionName": "China",
                        "ctryRegionCode": "CN",
                        "cityFullNameEn": "Shekou",
                        "cityFullNameCn": "蛇口"
                    }
                },
                "lastPodPort": {
                    "id": "349647918206992",
                    "portName": "Durban",
                    "portCode": "DUR",
                    "portFullNameEn": "Durban",
                    "portFullNameCn": "Durban",
                    "city": {
                        "id": "738872886271836",
                        "unlocode": "ZADUR",
                        "cityName": "Durban",
                        "cntyName": null,
                        "stateName": "KwaZulu-Natal",
                        "stateCode": null,
                        "ctryRegionName": "South Africa",
                        "ctryRegionCode": "ZA",
                        "cityFullNameEn": "Durban",
                        "cityFullNameCn": "德班"
                    }
                },
                "scheduleData": {
                    "origin": {
                        "id": "738872886233044",
                        "unlocode": "CNSHE",
                        "cityName": "Shekou",
                        "cntyName": "Shenzhen",
                        "stateName": "Guangdong",
                        "stateCode": "GD",
                        "ctryRegionName": "China",
                        "ctryRegionCode": "CN",
                        "cityFullNameEn": "Shekou",
                        "cityFullNameCn": "蛇口"
                    },
                    "destination": {
                        "id": "738872886271836",
                        "unlocode": "ZADUR",
                        "cityName": "Durban",
                        "cntyName": null,
                        "stateName": "KwaZulu-Natal",
                        "stateCode": null,
                        "ctryRegionName": "South Africa",
                        "ctryRegionCode": "ZA",
                        "cityFullNameEn": "Durban",
                        "cityFullNameCn": "德班"
                    },
                    "porFacilityCode": null,
                    "fndFacilityCode": null,
                    "legs": [
                        {
                            "carrier": null,
                            "service": {
                                "serviceCode": "AEM5",
                                "serviceId": null
                            },
                            "vessel": {
                                "vesselName": "COSCO YANTIAN",
                                "vesselId": null,
                                "vesselCode": null
                            },
                            "voyageCode": null,
                            "internalVoyageNumber": "020",
                            "externalVoyageNumber": "020W",
                            "pol": {
                                "port": {
                                    "id": "349645770723389",
                                    "portName": "Shekou",
                                    "portCode": "SHK",
                                    "portFullNameEn": "Shekou",
                                    "portFullNameCn": "蛇口",
                                    "city": null
                                },
                                "etd": "2020-08-04 00:00",
                                "atd": null,
                                "eta": null,
                                "ata": null,
                                "facilityCode": null
                            },
                            "pod": {
                                "port": {
                                    "id": "349647918206992",
                                    "portName": "Durban",
                                    "portCode": "DUR",
                                    "portFullNameEn": "Durban",
                                    "portFullNameCn": "Durban",
                                    "city": null
                                },
                                "etd": null,
                                "atd": null,
                                "eta": "2020-09-07 00:00",
                                "ata": null,
                                "facilityCode": null
                            },
                            "legSequence": 0,
                            "direction": "W",
                            "transportMode": null
                        }
                    ],
                    "cutOffLocalDate": null,
                    "estimatedTransitTimeInDays": 30,
                    "etaAtFnd": "2020-09-07 00:00",
                    "etdAtPor": "2020-08-04 00:00",
                    "serviceCode": "AEM5",
                    "vesselName": "COSCO YANTIAN",
                    "direction": "W",
                    "voyageNo": "020"
                },
                "routeProductPricingList": [
                    {
                        "cntrType": "20GP",
                        "price": 2.00,
                        "tradePrice": 0.50, //tradePrice = price - promotions - discounts
                        "currency": "USD",
                    }
                ],
                "promotions": {
                    "detail": [
                        {
                            "promotionType": "OFF",
                            "remnantInventory": 40,
                            "summary": "订单下箱型20GP、40GP、40HQ每UNIT立减1USD。",
                            "priority": 1
                        }
                    ]
                },
                "companyDiscountsInfoDTO": [
                    {
                        "channelCntr": "20GP",
                        "amount": 0.5,
                        "currencyType": "USD"
                    }
                ]
            }
        ],
        "number": 1,
        "size": 20,
        "totalPages": 1,
        "totalElements": 1,
        "first": true, // is first page
        "last": true, // is last page
        "empty": false
    }
}
```

### 6. Error sample

```javascript
{
    "code": 20000,
    "message": "cannot identify the user, please contact support for help"
}
```

## Intermodal Service Query Interface


### 1.Informations

* **Path**: /service/synconhub/common/intermodalService/{productId}
* **Method**: GET

### 2. Path parameters

|   Name    | **Type** | Required | Description |
| :-------: | :------: | :------: | :---------: |
| productId |  string  |   yes    | product id  |

### 3. Request sample

```HTTP
GET /service/synconhub/common/intermodalService/8a5e11157351b2df01735562622a0000
```

### 4. Response parameters

| Name    | Description    |
| ------- | -------------- |
| code    | status code    |
| message | detail message |
| data    | response data  |

### 5. Response sample

```javascript
{
    "code": 0,
    "message": "",
    "data": {
        "loadingServices": [    // loading port list
            {
                "transshipmentModel": "TRUCK+RAIL", // Transport Way eg:['FEEDER','RAIL','TRUCK','BARGE','TRUCK+RAIL']
                "cityUUID": "738872886232847", 
                "cityFullName": "青岛",
                "cityFullNameEn": "Qingdao",
                "ctryCode": "CN",
                "facilityCode": null,
                "facilityName": null,
                "bound": "O", // bound type,loading->O , discharge->I
                "transportTerms": "CY", // Traffic Mode 
                "bargeDay": 3,
                "transitDay": 4,
                "containerInfoDTOList": [ // Supported cntr type and weight config
                    {
                        "cntrSizeType": "20GP",
                        "cntrPrice": 100,
                        "currencyType": "USD",
                        "minWeight": null,
                        "maxWeight": null,
                        "weightUnit": null
                    },
                    {
                        "cntrSizeType": "40GP",
                        "cntrPrice": 300,
                        "currencyType": "USD",
                        "minWeight": null,
                        "maxWeight": null,
                        "weightUnit": null
                    },
                    {
                        "cntrSizeType": "40HQ",
                        "cntrPrice": 300,
                        "currencyType": "USD",
                        "minWeight": null,
                        "maxWeight": null,
                        "weightUnit": null
                    }
                ],
                "intermodalServiceNo": "8a5e112478d41b380178d951babe0032" // intermodal ServiceNo
            },
            {
                "transshipmentModel": "RAIL",
                "cityUUID": "738872886232873",
                "cityFullName": "上海",
                "cityFullNameEn": "Shanghai",
                "ctryCode": "CN",
                "facilityCode": "NGB05",
                "facilityName": "Ningbo Yuandong Terminals Limited",
                "bound": "O",
                "transportTerms": "CY",
                "bargeDay": 2,
                "transitDay": 3,
                "containerInfoDTOList": [
                    {
                        "cntrSizeType": "20GP",
                        "cntrPrice": 100,
                        "currencyType": "USD",
                        "minWeight": null,
                        "maxWeight": null,
                        "weightUnit": null
                    },
                    {
                        "cntrSizeType": "40GP",
                        "cntrPrice": 200,
                        "currencyType": "USD",
                        "minWeight": null,
                        "maxWeight": null,
                        "weightUnit": null
                    },
                    {
                        "cntrSizeType": "40HQ",
                        "cntrPrice": 200,
                        "currencyType": "USD",
                        "minWeight": null,
                        "maxWeight": null,
                        "weightUnit": null
                    }
                ],
                "intermodalServiceNo": "8a5e112478d41b380178d946dac1001c"
            },
            {
                "transshipmentModel": "TRUCK",
                "cityUUID": "738872886232847",
                "cityFullName": "青岛",
                "cityFullNameEn": "Qingdao",
                "ctryCode": "CN",
                "facilityCode": null,
                "facilityName": null,
                "bound": "O",
                "transportTerms": "CY",
                "bargeDay": 2,
                "transitDay": 4,
                "containerInfoDTOList": [ 
                    // If cargo weight information is configured,The same cntr type will have multiple data
                    {
                        "cntrSizeType": "20GP",
                        "cntrPrice": 100,
                        "currencyType": "USD",
                        "minWeight": 0,  // min cargo weight
                        "maxWeight": 20, // max cargo weight
                        "weightUnit": "TON" // weight unit eg: "TON"
                        // If the weight of the cargo is between 0-20 tons, the loading service fee is 100 USD
                    },
                    {
                        "cntrSizeType": "20GP",
                        "cntrPrice": 150,
                        "currencyType": "USD",
                        "minWeight": 20,
                        "maxWeight": 40,
                        "weightUnit": "TON"
                        // If the weight of the cargo is between 20-40 tons, the loading service fee is 150 USD
                    },
                    {
                        "cntrSizeType": "20GP",
                        "cntrPrice": 200,
                        "currencyType": "USD",
                        "minWeight": 40,
                        "maxWeight": null,
                        "weightUnit": "TON"
                        // If the weight of the cargo is more than 40 tons, the loading service fee is 100 USD
                    },
                    {
                        "cntrSizeType": "40GP",
                        "cntrPrice": 300,
                        "currencyType": "USD",
                        "minWeight": null,
                        "maxWeight": null,
                        "weightUnit": "TON"
                    },
                    {
                        "cntrSizeType": "40HQ",
                        "cntrPrice": 300,
                        "currencyType": "USD",
                        "minWeight": null,
                        "maxWeight": null,
                        "weightUnit": "TON"
                    }
                ],
                "intermodalServiceNo": "8a5e112478d41b380178d9516714002b"
            }
        ],
        "dischargeServices": [  // discharge port list
            {
                "transshipmentModel": "BARGE",
                "cityUUID": "738874496843873",
                "cityFullName": "Gwadar",
                "cityFullNameEn": "Gwadar",
                "ctryCode": "PK",
                "facilityCode": null,
                "facilityName": null,
                "bound": "I",
                "transportTerms": "FOR",
                "bargeDay": 19,
                "transitDay": 5,
                "containerInfoDTOList": [
                    {
                        "cntrSizeType": "20GP",
                        "cntrPrice": 300,
                        "currencyType": "USD",
                        "minWeight": null,
                        "maxWeight": null,
                        "weightUnit": null
                    },
                    {
                        "cntrSizeType": "40GP",
                        "cntrPrice": 400,
                        "currencyType": "USD",
                        "minWeight": null,
                        "maxWeight": null,
                        "weightUnit": null
                    },
                    {
                        "cntrSizeType": "40HQ",
                        "cntrPrice": 400,
                        "currencyType": "USD",
                        "minWeight": null,
                        "maxWeight": null,
                        "weightUnit": null
                    }
                ],
                "intermodalServiceNo": "8a5e112478d41b380178d94e5a4e0026"
            }
        ]
    }
}
```



### 6. Error sample

```javascript
{
    "code": 20002,
    "message": "The product cannot be found"
}
```



## Spot Rate Details Query Interface

### 1. Informations


* **Path**: /service/synconhub/product/instantBooking/{productId}
* **Method**: GET

### 2. Path parameters

|   Name    | **Type** | Required | Description |
| :-------: | :------: | :------: | :---------: |
| productId |  string  |   yes    | product id  |

### 3. Query parameters

|        Name        | **Type** | Required |                         Description                          |
| :----------------: | :------: | :------: | :----------------------------------------------------------: |
|  loadingServiceNo  |  String  |    no    | intermodalServiceNo from intermodal service query results. Get the spot rate details with loading service |
| dischargeServiceNo |  String  |    no    | intermodalServiceNo from intermodal service query results. Get the spot rate details with discharge service |


### 4. Request sample

```http
GET /service/synconhub/product/instantBooking/8a5e11157351b2df01735562622a0000
```

```http
GET /service/synconhub/product/instantBooking/8a5e11157351b2df01735562622a0000?loadingServiceNo=8a5e112478d41b380178d4551a33000a
```

```http
GET /service/synconhub/product/instantBooking/8a5e11157351b2df01735562622a0000?loadingServiceNo=8a5e112478d41b380178d4551a33000a&dischargeServiceNo=8a5e112478c4eed60178c51cca9f0001
```



### 5. Response parameters

| Name    | Description         |
| ------- | ------------------- |
| code    | status code         |
| message | detail message      |
| data    | product detail info |

### 6. Response sample

```javascript
{
    "code": 0,
    "message": "",
    "data": {
        "id": "8a5e11157351b2df01735562622a0000",
        "tradeLaneCode": "AEU",
        "serviceCode": "AEU2",
        "tradeArea": "ETD",
        "vesselName": "APL VANDA",
        "voyageNo": "022",
        "direction": "W",
        "porFacilityCode": "SHA08",
        "porFacilityName": "Shanghai Shengdong (I), Yangshan",
        "porFacilityNameCn": null,
        "fndFacilityName": "Eurogate Terminal",
        "fndFacilityNameCn": null,
        "fndFacilityCode": "HAM02",
        "haulageType": "CY-CY",
        "estimatedTransitTimeInDays": 30,
        "effectiveStartDate": "2020-07-05T00:00:00.000Z",
        "effectiveEndDate": "2020-12-23T23:59:59.000Z",
        "eta": "2020-09-16 00:00",
        "etd": "2020-08-12 00:00",
        "inventory": 396,
        "polTsMode": null, // pol transit mode
        "podTsMode": null, // pod transit mode
        "porCity": {  // por city info，for the specific structure, please refer to 'City information query interface'
            "id": "738872886232873",
            "unlocode": "CNSHA",
            "cityName": "Shanghai",
            "cntyName": "Shanghai",
            "stateName": "Shanghai",
            "stateCode": "SH",
            "ctryRegionName": "China",
            "ctryRegionCode": "CN",
            "cityFullNameEn": "Shanghai",
            "cityFullNameCn": "上海"
        },
        "fndCity": {
            "id": "738872886254648",
            "unlocode": "DEHAM",
            "cityName": "Hamburg",
            "cntyName": null,
            "stateName": "Hamburg",
            "stateCode": "HH",
            "ctryRegionName": "Germany",
            "ctryRegionCode": "DE",
            "cityFullNameEn": "Hamburg",
            "cityFullNameCn": "汉堡"
        },
        "polPort": {
            "id": "349657045012458",
            "portName": "Shanghai",
            "portCode": "SHA",
            "portFullNameEn": "Shanghai",
            "portFullNameCn": "上海",
            "city": {
                "id": "738872886232873",
                "unlocode": "CNSHA",
                "cityName": "Shanghai",
                "cntyName": "Shanghai",
                "stateName": "Shanghai",
                "stateCode": "SH",
                "ctryRegionName": "China",
                "ctryRegionCode": "CN",
                "cityFullNameEn": "Shanghai",
                "cityFullNameCn": "上海"
            }
        },
        "podPort": {
            "id": "349645770723600",
            "portName": "Hamburg",
            "portCode": "HAM",
            "portFullNameEn": "Hamburg",
            "portFullNameCn": "Hamburg",
            "city": {
                "id": "738872886254648",
                "unlocode": "DEHAM",
                "cityName": "Hamburg",
                "cntyName": null,
                "stateName": "Hamburg",
                "stateCode": "HH",
                "ctryRegionName": "Germany",
                "ctryRegionCode": "DE",
                "cityFullNameEn": "Hamburg",
                "cityFullNameCn": "汉堡"
            }
        },
        "firstPolPort": {
            "id": "349657045012458",
            "portName": "Shanghai",
            "portCode": "SHA",
            "portFullNameEn": "Shanghai",
            "portFullNameCn": "上海",
            "city": {
                "id": "738872886232873",
                "unlocode": "CNSHA",
                "cityName": "Shanghai",
                "cntyName": "Shanghai",
                "stateName": "Shanghai",
                "stateCode": "SH",
                "ctryRegionName": "China",
                "ctryRegionCode": "CN",
                "cityFullNameEn": "Shanghai",
                "cityFullNameCn": "上海"
            }
        },
        "lastPodPort": {
            "id": "349645770723600",
            "portName": "Hamburg",
            "portCode": "HAM",
            "portFullNameEn": "Hamburg",
            "portFullNameCn": "Hamburg",
            "city": {
                "id": "738872886254648",
                "unlocode": "DEHAM",
                "cityName": "Hamburg",
                "cntyName": null,
                "stateName": "Hamburg",
                "stateCode": "HH",
                "ctryRegionName": "Germany",
                "ctryRegionCode": "DE",
                "cityFullNameEn": "Hamburg",
                "cityFullNameCn": "汉堡"
            }
        },
        "scheduleData": { 
            "origin": {
                "id": "738872886232873",
                "unlocode": "CNSHA",
                "cityName": "Shanghai",
                "cntyName": "Shanghai",
                "stateName": "Shanghai",
                "stateCode": "SH",
                "ctryRegionName": "China",
                "ctryRegionCode": "CN",
                "cityFullNameEn": "Shanghai",
                "cityFullNameCn": "上海"
            },
            "destination": {
                "id": "738872886254648",
                "unlocode": "DEHAM",
                "cityName": "Hamburg",
                "cntyName": null,
                "stateName": "Hamburg",
                "stateCode": "HH",
                "ctryRegionName": "Germany",
                "ctryRegionCode": "DE",
                "cityFullNameEn": "Hamburg",
                "cityFullNameCn": "汉堡"
            },
            "porFacilityCode": "SHA08", 
            "fndFacilityCode": "HAM02",
            "legs": [
                {
                    "carrier": null,
                    "service": {
                        "serviceCode": "AEU2",
                        "serviceId": null
                    },
                    "vessel": {
                        "vesselName": "APL VANDA",
                        "vesselId": null,
                        "vesselCode": null
                    },
                    "voyageCode": null,
                    "internalVoyageNumber": "022",
                    "externalVoyageNumber": "022W",
                    "pol": {
                        "port": {
                            "id": "349657045012458",
                            "portName": "Shanghai",
                            "portCode": "SHA",
                            "portFullNameEn": "Shanghai",
                            "portFullNameCn": "上海",
                            "city": null
                        },
                        "etd": "2020-08-12 00:00",
                        "atd": null,
                        "eta": null,
                        "ata": null,
                        "facilityCode": "SHA08"
                    },
                    "pod": {
                        "port": {
                            "id": "349645770723600",
                            "portName": "Hamburg",
                            "portCode": "HAM",
                            "portFullNameEn": "Hamburg",
                            "portFullNameCn": "Hamburg",
                            "city": null
                        },
                        "etd": null,
                        "atd": null,
                        "eta": "2020-09-16 00:00",
                        "ata": null,
                        "facilityCode": "HAM02"
                    },
                    "legSequence": 0,
                    "direction": "W",
                    "transportMode": null
                }
            ],
            "cutOffLocalDate": null,
            "estimatedTransitTimeInDays": 30,
            "etaAtFnd": "2020-09-16 00:00",
            "etdAtPor": "2020-08-12 00:00",
            "serviceCode": "AEU2",
            "vesselName": "APL VANDA",
            "direction": "W",
            "voyageNo": "022"
        },
        "routeProductPricingList": [
            {
                "cntrType": "20GP",
                "price": 2.00, 
                "tradePrice": 0.50,
                "currency": "USD"
            }
        ],
        "promotions": {
            "detail": [
                {
                    "promotionType": "OFF", 
                    "remnantInventory": 51,
                    "summary": "订单下箱型20GP、40GP、40HQ每UNIT立减1USD。",
                    "priority": 1, // usage priority
                }
            ]
        },
        "companyDiscountsInfoDTO": [
            {
                "channelCntr": "20GP",
                "amount": 0.5,
                "currencyType": "USD",
            }
        ],
        "couponInfos": [
            {
                "couponId": "8a5e11227152917a017152b6049f0002",
                "inventory": 10,
                "displayName": "test",
                "value": 1,
                "currency": "USD",
                "minOceanFee": 1, // minimum use interval of ocean fee
                "maxOceanFee": 10, // maximum use interval of ocean fee
                "maxUseLimit": 2, // use limit
                "usageUnit": "UNIT"
            }
        ],
    }
}
```

### 7. Error sample

```javascript
{
    "code": 20000,
    "message": "cannot identify the user, please contact support for help"
}
```



## Surcharge Query Interface

Get the surcharge details of a specific product.

### 1. Informations


* **Path**: /service/synconhub/common/extraChargeFee/${productId}
* **Method**: GET

### 2. Path parameters

|   Name    | **Type** | Required | Description |
| :-------: | :------: | :------: | :---------: |
| productId |  String  |   yes    | product id  |

### 3. Query parameters

|        Name        | **Type** | Required |                         Description                          |
| :----------------: | :------: | :------: | :----------------------------------------------------------: |
|  loadingServiceNo  |  String  |    no    | intermodal Service No from intermodal service query results. Get the surcharge details of a specific product with loading service |
| dischargeServiceNo |  String  |    no    | intermodal Service No from intermodal service query results. Get the surcharge details of a specific product with discharge service |

### 4. Request sample

```http
GET /service/synconhub/common/extraChargeFee/8a80cb817321ad030173221c880101d2
```

```http
GET /service/synconhub/common/extraChargeFee/8a80cb817321ad030173221c880101d2?loadingServiceNo=8a5e11127467f02501747218e25c0001
```

```http
GET /service/synconhub/common/extraChargeFee/8a80cb817321ad030173221c880101d2?loadingServiceNo=8a5e11127467f02501747218e25c0001&dischargeServiceNo=8a5e11247866fbec0178681acd3a0008
```



### 5. Response parameters

| Name    | Description    |
| ------- | -------------- |
| code    | status code    |
| message | detail message |
| data    | response data  |

### 6. Response sample

```javascript
{
    "code": 0,
    "message": "",
    "data": [
                {
                    "chargeModel": "BL",
                    "cntrSize": null, // container type
                    "chargeDetail": [ // detail info
                        {
                            "chargeCode": "DOC",
                            "chargeName": "DOC",
                            "chargeType": "DOC",
                            "chargeTag": "POR",
                            "price": 666,
                            "currency": "USD",
                            "category": null // charge category
                        }
                    ]
                },
                {
                    "chargeModel": "CNTR",
                    "cntrSize": "20GP",
                    "chargeDetail": [
                        {
                            "chargeCode": "255",
                            "chargeName": "工本费",
                            "chargeType": "CNTR_LOCAL",
                            "chargeTag": "POR",
                            "price": 10,
                            "currency": "USD",
                            "category": "EXTRA_CHARGE"
                        }
                    ]
                }
    		]
}
```

### 7. Error sample

```javascript
{
    "code": 20001,
    "message": "cannot proceed the request, please retry later or contact support for help"
}
```

```javascript
{
    "code": 20059,
    "message": "Wrong bound type of intermodal service"
}
```

```javascript
{
    "code": 20060,
    "message": "The intermodal service does not exist"
}
```

