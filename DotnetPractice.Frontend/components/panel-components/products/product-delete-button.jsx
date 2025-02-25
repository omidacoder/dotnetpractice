/* eslint-disable prettier/prettier */
import { Button, Popover, PopoverContent, PopoverTrigger } from "@heroui/react";
import { RiDeleteBin6Fill } from "react-icons/ri";
import React from "react";
import { useMutation, useQueryClient } from "@tanstack/react-query";
import { toast } from "react-toastify";

import { DeleteProduct } from "@/requests/mutations/product/DeleteProduct";

export default function ProductDeleteButton({ id }) {
  const [isOpen, setIsOpen] = React.useState(false);
  const [loading, setLoading] = React.useState(false);
  const queryClient = useQueryClient();
  const mutation = useMutation({
    mutationFn: DeleteProduct,
    onSuccess: (_response) => {
      setLoading(false);
      toast("Deleted Successfully", {
        type: "success",
        position: "bottom-center",
        hideProgressBar: false,
        closeOnClick: true,
        pauseOnHover: true,
        draggable: true,
        autoClose: 3000,
      });
      setIsOpen(false);
      queryClient.invalidateQueries({ queryKey: ["products"] });
    },
    onError: (error) => {
      setLoading(false);
      handleError(error, toast);
    },
  });
  const deleteProduct = () => {
    setLoading(true);
    mutation.mutate({ id });
  };

  return (
    <Popover
      isOpen={isOpen}
      placement="bottom"
      showArrow={true}
      onOpenChange={(open) => setIsOpen(open)}
    >
      <PopoverTrigger>
        <span className="text-lg text-danger cursor-pointer active:opacity-50">
          <RiDeleteBin6Fill className="mr-2" size={20} />
        </span>
      </PopoverTrigger>
      <PopoverContent>
        <div className="px-1 py-3">
          <div className="text-small font-bold primary_color">
            Are you sure?
          </div>
          <div className="mt-2">
            <Button
              className="mx-2"
              color="primary"
              isLoading={loading}
              variant="flat"
              onPress={deleteProduct}
            >
              Yes
            </Button>
            <Button
              className="mx-2"
              color="danger"
              isDisabled={loading}
              variant="light"
              onPress={() => setIsOpen(false)}
            >
              No
            </Button>
          </div>
        </div>
      </PopoverContent>
    </Popover>
  );
}
