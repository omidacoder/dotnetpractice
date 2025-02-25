/* eslint-disable prettier/prettier */
import React, { useRef } from "react";
import { Button, Card, CardBody, Input, Textarea } from "@heroui/react";
export default function ProductSearchForm({
  setName,
  setDescription,
  setMinPrice,
  setMaxPrice,
}) {
  const nameRef = useRef();
  const descriptionRef = useRef();
  const minPriceRef = useRef();
  const maxPriceRef = useRef();
  const search = () => {
    setName(nameRef.current.value);
    setDescription(descriptionRef.current.value);
    setMinPrice(minPriceRef.current.value);
    setMaxPrice(maxPriceRef.current.value);
  };
  const reset = () => {
    setName(null);
    setDescription(null);
    setMinPrice(null);
    setMaxPrice(null);
  };
  return (
    <div className="w-full mt-3 p-3">
      <Card className="mb-2">
        <CardBody>
          <div className="w-full mb-2 text-center">
            <h2>Search Box</h2>
          </div>
          <form>
            <div className="w-full grid grid-cols-12 gap-2 py-2 items-center justify-around ">
              <div className="col-span-12 md:col-span-6 mt-2">
                <Input
                  ref={nameRef}
                  label="Name"
                  type="text"
                  variant="bordered"
                />
              </div>
              <div className="col-span-12 mt-2">
                <Textarea
                  ref={descriptionRef}
                  className="md:col-span-6 mb-6 md:mb-0"
                  label="Descriptions"
                  variant="bordered"
                />
              </div>
            </div>
            <div className="w-full grid grid-cols-12 gap-2 py-2 items-center justify-around ">
              <div className="col-span-3 md:col-span-2 mt-1">
                <h5> Price (USD) : </h5>
              </div>
              <div className="col-span-1 mt-1">
                <div className="text-center">
                  <h5>From</h5>
                </div>
              </div>
              <div className="col-span-2 mt-1">
                <Input
                  ref={minPriceRef}
                  label="Min"
                  type="number"
                  variant="bordered"
                />
              </div>
              <div className="col-span-1 mt-1">
                <div className="text-center">
                  <h5>To</h5>
                </div>
              </div>
              <div className="col-span-2 mt-1">
                <Input
                  ref={maxPriceRef}
                  label="Max"
                  type="number"
                  variant="bordered"
                />
              </div>
            </div>
            <div
              style={{
                display: "flex",
                justifyContent: "center",
                marginTop: 10,
              }}
            >
              <Button
                className="mx-2"
                color="primary"
                variant="flat"
                onPress={search}
              >
                Search
              </Button>
              <Button
                className="mx-2"
                color="warning"
                variant="flat"
                onPress={reset}
              >
                Reset
              </Button>
            </div>
          </form>
        </CardBody>
      </Card>
    </div>
  );
}
