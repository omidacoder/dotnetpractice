/* eslint-disable prettier/prettier */
import React from "react";
import { CgMenu } from "react-icons/cg";
import Link from "next/link";
import { FaRegTrashCan, FaSolarPanel } from "react-icons/fa6";
import { IoExit } from "react-icons/io5";
import {
  Drawer,
  DrawerContent,
  DrawerHeader,
  DrawerBody,
  useDisclosure,
} from "@heroui/react";
import { useTheme } from "next-themes";
export default function SideMenu() {

  const { theme } = useTheme();
  const { isOpen, onOpen, onClose } = useDisclosure();

  return (
    <>
      <button onClick={onOpen}>
        <CgMenu className="primary_color" size={30} />
      </button>
      <Drawer isOpen={isOpen} placement="left" size="sm" onClose={onClose}>
        <DrawerContent>
          {(_onClose) => (
            <>
              <DrawerHeader className="flex flex-col gap-1">
                Welcome to the panel
              </DrawerHeader>
              <DrawerBody>
                <div className="flex-column mt-4">
                  <div
                    className={
                      "w-full grid grid-cols-12 gap-2 m-0 p-2 cursor-pointer " +
                      (theme == "light"
                        ? "side-menu-item"
                        : "side-menu-item-dark")
                    }
                  >
                    <div className="col-span-2">
                      <FaSolarPanel size={20} />
                    </div>
                    <div className="col-span-10">
                      <Link href="/panel/dashboard">Dashboard</Link>
                    </div>
                  </div>

                  <div
                    className={
                      "w-full grid grid-cols-12 gap-2 m-0 p-2 cursor-pointer " +
                      (theme == "light"
                        ? "side-menu-item"
                        : "side-menu-item-dark")
                    }
                  >
                    <div className="col-span-2">
                      <FaRegTrashCan size={20} />
                    </div>
                    <div className="col-span-10">
                      <Link href="/panel/products">Products</Link>
                    </div>
                  </div>
                  <div
                    className={
                      "w-full grid grid-cols-12 gap-2 m-0 p-2 cursor-pointer " +
                      (theme == "light"
                        ? "side-menu-item"
                        : "side-menu-item-dark")
                    }
                  >
                    <div className="col-span-2">
                      <IoExit size={20} />
                    </div>
                    <div className="col-span-10">
                      <Link
                        href={"/"}
                        onClick={() => {
                          localStorage.removeItem("tk");
                          localStorage.removeItem("name");
                          localStorage.removeItem("phone");
                        }}
                      >
                        Logout
                      </Link>
                    </div>
                  </div>
                </div>
              </DrawerBody>
            </>
          )}
        </DrawerContent>
      </Drawer>
    </>
  );
}
