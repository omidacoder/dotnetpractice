/* eslint-disable prettier/prettier */
import React, { useEffect, useState } from "react";
import {
  Table,
  TableHeader,
  TableColumn,
  TableBody,
  TableRow,
  TableCell,
  Tooltip,
  Spinner,
  Card,
  CardBody,
  Modal,
  ModalHeader,
  Divider,
  ModalBody,
  ModalFooter,
  Button,
  ModalContent,
  useDisclosure,
} from "@heroui/react";
import { TbPencilMinus } from "react-icons/tb";
import { useQuery } from "@tanstack/react-query";
import Image from "next/image";
import { CgEye } from "react-icons/cg";

import ProductSearchForm from "./product-search-form";
import ProductDeleteButton from "./product-delete-button";

import { GetProducts } from "@/requests/queries/product/GetProducts";
import { GetImageBinary } from "@/requests/queries/GetImageBinary";
export default function ProductsTable({
  ProductsEditModal,
  setEdittingProduct,
}) {
  const [name, setName] = React.useState();
  const [description, setDescription] = React.useState();
  const [minPrice, setMinPrice] = React.useState();
  const [maxPrice, setMaxPrice] = React.useState();
  const { isOpen, onOpenChange, onOpen} = useDisclosure();
  const [imageUrl, setImageUrl] = useState(null);
  const [imageBase64, setImageBase64] = useState(null);
  const columns = [
    { name: "Name", uid: "name" },
    { name: "Price", uid: "price" },
    {name: "Available", uid: "availableCount"},
    { name: "Descriptions", uid: "description" },
    { name: "Actions", uid: "actions" },
    
  ];
  const query = useQuery({
    queryKey: ["products", name, description],
    queryFn: async () =>
      await GetProducts({
        name,
        description,
        minPrice,
        maxPrice
      }),
  });


  const items = query.data?.data;
  useEffect(() => {
    const func = async () => {
      if (imageUrl) setImageBase64(await GetImageBinary(imageUrl));
    }
    func();
  }, [imageUrl])
  const renderCell = React.useCallback((item, columnKey) => {
    const cellValue = item[columnKey];
    const observe = () => {
      setImageUrl(item["photoUrl"]);
      onOpen();
    };
    switch (columnKey) {
      case "actions":
        return (
          <div className="relative flex flex-row items-center justify-center gap-2">
            <Tooltip content="Show Photo">
              <span className="text-lg text-default-400 cursor-pointer active:opacity-50">
                <CgEye className="mr-2" size={20} onClick={observe} />
              </span>
            </Tooltip>
            <Tooltip content="Edit">
              <span className="text-lg text-default-400 cursor-pointer active:opacity-50">
                <TbPencilMinus
                  className="mr-2"
                  size={20}
                  onClick={() => {
                    ProductsEditModal(true);
                    setEdittingProduct(item);
                  }}
                />
              </span>
            </Tooltip>
            <Tooltip content="Delete">
              <span className="text-lg text-default-400 cursor-pointer active:opacity-50">
                <ProductDeleteButton id={item.id} />
              </span>
            </Tooltip>
          </div>
        );
      case "price":
        return (
          <div className="flex justify-start">
            <p className="text-bold text-left text-sm">
              {cellValue + " USD"}
            </p>
          </div>
        );
      default:
        return (
          <div className="flex justify-start">
            <p className="text-bold text-sm text-left">
              {cellValue}
            </p>
          </div>
        );
    }
  }, []);

  return (
    <>
      <Modal
        hideCloseButton
        isOpen={isOpen}
        placement="top"
        scrollBehavior="outside"
        size="md"
        onOpenChange={onOpenChange}
      >
        <ModalContent>
          {(onClose) => (
            <>
              <ModalHeader className="flex flex-col gap-1">
                Product Image
              </ModalHeader>
              <Divider />
              <ModalBody>
                <div className="w-full mt-5 justify-center py-2 items-center">
                  {imageUrl ? (
                    <Image
                      alt="Product has no image"
                      height={300}
                      src={"data:image;base64," +imageBase64}
                      style={{width: '100%', height: 'auto'}}
                      width={300}
                    />
                  ) : (
                    <p>This Product Has No Image</p>
                  )}
                </div>
              </ModalBody>
              <ModalFooter>
                <Button color="danger" variant="light" onPress={onClose}>
                  Close
                </Button>
              </ModalFooter>
            </>
          )}
        </ModalContent>
      </Modal>
      <ProductSearchForm
        setDescription={setDescription}
        setMaxPrice={setMaxPrice}
        setMinPrice={setMinPrice}
        setName={setName}
      />
      <div className="w-full mt-5">
        <Card className="mb-2">
          <CardBody
            style={{
              minHeight: "80vh",
              maxHeight: "100vh",
              overflowY: "scroll",
            }}
          >
            <Table isCompact removeWrapper>
              <TableHeader columns={columns}>
                {(column) => (
                  <TableColumn
                    key={column.uid}
                    align="center"
                    className="text-bold h6"
                    style={{
                      textAlign: "left",
                    }}
                  >
                    {column.name}
                  </TableColumn>
                )}
              </TableHeader>
              <TableBody
                isLoading={query.isLoading}
                items={items ?? []}
                loadingContent={<Spinner />}
                loadingState={query.isPending}
              >
                {(item) => (
                  <TableRow key={item.id}>
                    {(columnKey) => (
                      <TableCell>{renderCell(item, columnKey)}</TableCell>
                    )}
                  </TableRow>
                )}
              </TableBody>
            </Table>
          </CardBody>
        </Card>
      </div>
    </>
  );
}
